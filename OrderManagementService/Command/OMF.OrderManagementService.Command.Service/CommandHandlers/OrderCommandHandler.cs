using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using OMF.Common.Abstractions;
using OMF.Common.Enums;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class OrderCommandHandler : IRequestHandler<OrderCommand, Response>,
        IRequestHandler<OrderUpdateCommand, Response>
    {
        private readonly IEventBus _bus;
        private readonly IConfiguration _configuration;
        private readonly IHttpWrapper _http;
        private readonly IMapper _map;
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(IEventBus bus, IOrderRepository orderRepository, IMapper map, IHttpWrapper http,
            IConfiguration configuration)
        {
            _bus = bus;
            _orderRepository = orderRepository;
            _map = map;
            _http = http;
            _configuration = configuration;
        }

        /// <summary>
        /// Handler create food order
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Order Id</returns>
        public async Task<Response> Handle(OrderCommand command, CancellationToken cancellationToken)
        {
            var response = new Response(200, "Order added to cart");
            //Get restaurant details
            var restaurant = (await _http.Get<IEnumerable<Restaurant>>(string.Format(_configuration["RestaurantURL"], command.RestaurantId))).FirstOrDefault();
            if (restaurant == null)
            {
                response.Code = 400;
                response.Message = "Restaurant not found";
            }

            //Add order to cart
            var order = _map.Map<TblFoodOrder>(command);
            await _orderRepository.Create(order);
            
            if (command.BookNow)
            {
                order.Status = OrderStatus.PaymentPending.ToString();
                response.Message = $"Order placed successfully. Order Id: {order.Id}";
            }

            //Add order items
            foreach (var item in command.OrderItems)
            {
                var offer = restaurant.Offers.FirstOrDefault(x => x.ItemId == item.MenuId);
                if (offer == null)
                {
                    await _orderRepository.Delete(order);
                    return new Response(400, $"Item: {item.MenuId} not found");
                }

                if ( item.Quantity <= offer.Quantity)
                {
                    order.TblFoodOrderItem.Add(new TblFoodOrderItem
                    {
                        TblFoodOrderId = order.Id,
                        TblMenuId = item.MenuId,
                        Quantity = item.Quantity,
                        Price = offer.Price,
                        CreatedDate = DateTime.UtcNow
                    });
                    continue;
                }

                await _orderRepository.Delete(order);
                response.Code = 400;
                response.Message = $"Item: {item.MenuId} out of stock";
                return response;
            }

            await _orderRepository.Update(order);
            if (command.BookNow)
                await _bus.PublishEvent(new PaymentInitiatedEvent(order.Id, "Food"));
            return response;
        }

        
        /// <summary>
        /// Handler Order update
        /// Add new items or change address
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var order = (await _orderRepository.Get<TblFoodOrder>(x=>x.Id==request.OrderId)).FirstOrDefault();

            if (order == null)
                return new Response(400, "Order doesn't exist ");

            if (order.PaymentId != 0 && request.OrderItems != null)
            {
                return new Response(400,"Order items cannot be modified after payment");
            }

            if (request.OrderItems != null)
            {
                order.TblFoodOrderItem =
                    (await _orderRepository.Get<TblFoodOrderItem>(x => x.TblFoodOrderId == request.OrderId)).ToList();
                var restaurant =
                    (await _http.Get<IEnumerable<Restaurant>>(string.Format(_configuration["RestaurantURL"],
                        order.TblRestaurantId))).FirstOrDefault();
                foreach (var item in request.OrderItems)
                {
                    var orderedItem = order.TblFoodOrderItem.FirstOrDefault(x => x.TblMenuId == item.MenuId);
                    if (orderedItem != null)
                    {
                        orderedItem.Quantity = item.Quantity;
                        continue;
                    }
                    var offer = restaurant.Offers.FirstOrDefault(x => x.ItemId == item.MenuId);
                    if (offer == null)
                    {
                        await _orderRepository.Delete(order);
                        return new Response(400, $"Item: {item.MenuId} not found");
                    }
                    order.TblFoodOrderItem.Add(new TblFoodOrderItem()
                    {
                        TblMenuId = item.MenuId,
                        Quantity = item.Quantity,
                        Price = offer.Price
                    });

                }
            }

            order.Address = request.Address;
            order.ModifiedDate = DateTime.UtcNow;

            await _orderRepository.Update(order);

            return new Response(200, "Order updated successfully");
            ;
        }
    }
}