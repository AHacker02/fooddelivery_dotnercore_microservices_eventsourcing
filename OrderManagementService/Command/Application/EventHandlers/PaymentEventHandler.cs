using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.OrderManagementService.Command.Application.Events;
using OMF.OrderManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace OMF.OrderManagementService.Command.Application.EventHandlers
{
    public class PaymentEventHandler : IEventHandler<PaymentInitiatedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _bus;

        public PaymentEventHandler(IOrderRepository orderRepository, IEventBus bus)
        {
            _orderRepository = orderRepository;
            _bus = bus;
        }

        public async Task HandleAsync(PaymentInitiatedEvent @event)
        {
            try
            {
                var random = new Random();
                var type = typeof(OrderStatus);
                var values = type.GetEnumValues();
                var order = await _orderRepository.GetOrder(@event.Id);
                //order.Status = ((OrderStatus) values.GetValue(random.Next(2, 3))).ToString();
                order.Status = OrderStatus.PaymentSuccessful.ToString();
                await _orderRepository.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }
    }
}