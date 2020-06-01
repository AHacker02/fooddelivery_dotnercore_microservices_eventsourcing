using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using OMF.Common.Abstractions;
using OMF.Common.Enums;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class OrderCommandHandler : IRequestHandler<OrderCommand, Response>, IRequestHandler<OrderUpdateCommand, Response>
    {
        private readonly IEventBus _bus;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _map;
        private readonly IHttpWrapper _http;
        private readonly IConfiguration _configuration;

        public OrderCommandHandler(IEventBus bus, IOrderRepository orderRepository, IMapper map, IHttpWrapper http, IConfiguration configuration)
        {
            _bus = bus;
            _orderRepository = orderRepository;
            _map = map;
            _http = http;
            _configuration = configuration;
        }

        public async Task<Response> Handle(OrderCommand command, CancellationToken cancellationToken)
        {
            var response = new Response(200, "Order added to cart");
            var restaurant = (await _http.Get<IEnumerable<Restaurant>>(string.Format(_configuration["RestaurantURL"], command.RestaurantId))).FirstOrDefault();
            if (restaurant == null)
            {
                response.Code = 400;
                response.Message = "Restaurant not found";
            }
            var order = await _orderRepository.CreateOrder(_map.Map<TblFoodOrder>(command));
            if (command.BookNow)
            {
                order.Status = OrderStatus.PaymentPending.ToString();
                response.Message = $"Order placed successfully. Order Id: {order.Id}";
            }
            foreach (var item in command.OrderItems)
            {
                if (item.Quantity <= restaurant.Offers.FirstOrDefault(x => x.ItemId == item.MenuId).Quantity)
                {
                    order.TblFoodOrderItem.Add(new TblFoodOrderItem()
                    {
                        TblFoodOrderId = order.Id,
                        TblMenuId = item.MenuId,
                        Quantity = item.Quantity,
                        Price = restaurant.Offers.FirstOrDefault(x => x.ItemId == item.MenuId).Price,
                        CreatedDate = DateTime.UtcNow
                    });
                    continue;
                }
                response.Code = 400;
                response.Message = $"Item: {item.MenuId} out of stock";
                return response;
            }

            await _orderRepository.UpdateOrder(order);
            if (command.BookNow)
                await _bus.PublishEvent(new PaymentInitiatedEvent(order.Id, "Food"));
            return response;


        }

        public async Task<Response> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetDetails<TblFoodOrder>(request.OrderId);

            if (order == null)
                return new Response(400, "Order doesn't exist ");

            order.Address = request.Address;
            order.ModifiedDate = DateTime.UtcNow;

            await _orderRepository.UpdateOrder(order);

            return new Response(200, "Order updated successfully"); ;
        }
    }
}