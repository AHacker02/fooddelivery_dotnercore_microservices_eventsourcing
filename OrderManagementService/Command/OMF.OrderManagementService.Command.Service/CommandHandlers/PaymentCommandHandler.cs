using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommand, Response>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _map;
        private readonly IEventBus _bus;

        public PaymentCommandHandler(IOrderRepository orderRepository, IMapper map, IEventBus bus)
        {
            _orderRepository = orderRepository;
            _map = map;
            _bus = bus;
        }
        public async Task<Response> Handle(PaymentCommand command, CancellationToken cancellationToken)
        {
            var transactionId = await _orderRepository.CreatePayment(_map.Map<TblOrderPayment>(command),command.Domain,command.OrderId);

            if (command.Domain == "Food")
            {
                var order = await _orderRepository.GetDetails<TblFoodOrder>(command.OrderId);
                await _bus.PublishEvent(new OrderConfirmedEvent(order.TblRestaurantId, _map.Map<IEnumerable<FoodOrderItem>>(order.TblFoodOrderItem)));
            }

            return new Response(200,$"Transaction was successful. Transaction Id: {transactionId}");

        }
    }
}