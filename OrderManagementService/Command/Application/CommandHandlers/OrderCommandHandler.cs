using AutoMapper;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Application.Commands;
using OMF.OrderManagementService.Command.Application.Events;
using OMF.OrderManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace OMF.OrderManagementService.Command.Application.CommandHandlers
{
    public class OrderCommandHandler : ICommandHandler<OrderCommand>
    {
        private readonly IEventBus _bus;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _map;

        public OrderCommandHandler(IEventBus bus, IOrderRepository orderRepository, IMapper map)
        {
            _bus = bus;
            _orderRepository = orderRepository;
            _map = map;
        }
        public async Task HandleAsync(OrderCommand command)
        {
            try
            {
                var order = _map.Map<Order>(command);
                order.Status = OrderStatus.PaymentPending.ToString();
                await _orderRepository.CreateOrder(order);
                await _bus.PublishEvent(new PaymentInitiatedEvent(order.Id));
                while (order.Status == OrderStatus.PaymentPending.ToString())
                {
                    order.Status = (await _orderRepository.GetOrder(order.Id)).Status;
                }

                if (order.Status == OrderStatus.PaymentSuccessful.ToString())
                {
                    order.Status = OrderStatus.OrderPlaced.ToString();
                    await _orderRepository.UpdateOrder(order);
                    await _bus.PublishEvent(new OrderConfirmedEvent(order.Id, order.RestaurantId, order.OrderItems,
                        order.Address));
                }
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception", $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }

        }
    }
}
