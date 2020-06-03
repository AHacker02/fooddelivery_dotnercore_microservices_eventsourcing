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
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommand, Response>
    {
        private readonly IEventBus _bus;
        private readonly IHttpWrapper _http;
        private readonly IConfiguration _configuration;
        private readonly IMapper _map;
        private readonly IOrderRepository _orderRepository;

        public PaymentCommandHandler(IOrderRepository orderRepository, IMapper map, IEventBus bus,IHttpWrapper http,IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _map = map;
            _bus = bus;
            _http = http;
            _configuration = configuration;
        }

        
        /// <summary>
        /// Handler create payment for order
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response> Handle(PaymentCommand command, CancellationToken cancellationToken)
        {
            TblFoodOrder foodOrder=null;
            //Calculate transaction ammount
            if (command.Domain == Domain.Food.ToString())
            {
                foodOrder = (await _orderRepository.Get<TblFoodOrder>(x => x.Id == command.OrderId)).FirstOrDefault();
                if (foodOrder == null)
                {
                    return new Response(400,"Order not found");
                }
                
                var restaurant =
                    (await _http.Get<IEnumerable<Restaurant>>(string.Format(_configuration["RestaurantURL"],
                        foodOrder.TblRestaurantId))).FirstOrDefault();

                foodOrder.TblFoodOrderItem = (await _orderRepository.Get<TblFoodOrderItem>(x => x.TblFoodOrderId == command.OrderId)).ToList();
                foreach (var orderItem in foodOrder.TblFoodOrderItem)
                {
                    var item = restaurant.Offers.FirstOrDefault(x => x.ItemId == orderItem.TblMenuId);
                    command.TransactionAmount += (DateTime.UtcNow > item.ToDate && DateTime.UtcNow < item.FromDate)
                        ? item.Price - item.Discount
                        : item.Price;
                }

            }
            else if(command.Domain==Domain.Table.ToString())
            {
                var tableOrder=(await _orderRepository.Get<TblTableBooking>(x => x.Id == command.OrderId)).FirstOrDefault();
                if (tableOrder == null)
                {
                    return new Response(400,"Order not found");
                }

                command.TransactionAmount = tableOrder.TblTableDetail.Sum(x => x.Price);
            }
            
            
            var transactionId = await _orderRepository.CreatePayment(_map.Map<TblOrderPayment>(command), command.Domain,
                command.OrderId);

            //Raise stock update event id food ordered
            if (command.Domain == "Food")
            {
                await _bus.PublishEvent(new OrderConfirmedEvent(foodOrder.TblRestaurantId,
                    _map.Map<IEnumerable<FoodOrderItem>>(foodOrder.TblFoodOrderItem)));
            }

            return new Response(200, $"Transaction was successful. Transaction Id: {transactionId}");
        }

    }
}