using System;
using System.Threading.Tasks;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Service.Commands;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _bus;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IEventBus bus)
        {
            _orderRepository = orderRepository;
            _bus = bus;
        }

        public async Task HandleAsync(CancelOrderCommand command)
        {
            try
            {
                var order = await _orderRepository.GetOrder(command.Id);
                if (order.UserId != command.UserId)
                {
                    await _bus.PublishEvent(new ExceptionEvent("user_unauthorized",
                        "User is not authorized to cancel the order", command));
                }

                if ((OrderStatus)Enum.Parse(typeof(OrderStatus), order.Status, true) <= OrderStatus.PaymentSuccessful)
                {
                    order.Status = OrderStatus.Cancelled.ToString();
                    await _orderRepository.UpdateOrder(order);
                }
                else
                {
                    await _bus.PublishEvent(new ExceptionEvent("cancellation_denied",
                        "Order cannot be cancelled", command));
                }

            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception", $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}
