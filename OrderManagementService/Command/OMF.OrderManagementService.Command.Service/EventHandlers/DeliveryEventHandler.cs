using System;
using System.Threading.Tasks;
using OMF.Common.Enums;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.EventHandlers
{
    public class DeliveryEventHandler : IEventHandler<OrderReadyEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _bus;

        public DeliveryEventHandler(IOrderRepository orderRepository, IEventBus bus)
        {
            _orderRepository = orderRepository;
            _bus = bus;
        }
        public async Task HandleAsync(OrderReadyEvent @event)
        {
            //try
            //{
            //    var order = await _orderRepository.GetOrder(@event.Id);
            //    order.Status = OrderStatus.Delivered.ToString();
            //    await _orderRepository.UpdateDetails(order);
            //}
            //catch (Exception ex)
            //{
            //    await _bus.PublishEvent(new ExceptionEvent("system_exception",
            //        $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            //}
        }
    }
}
