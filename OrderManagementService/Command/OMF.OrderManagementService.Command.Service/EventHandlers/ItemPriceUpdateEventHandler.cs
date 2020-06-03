using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.EventHandlers
{
    public class ItemPriceUpdateEventHandler : IEventHandler<ItemPriceUpdateEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _bus;
        private readonly ILogger<ItemPriceUpdateEventHandler> _logger;

        public ItemPriceUpdateEventHandler(IOrderRepository orderRepository,IEventBus bus, ILogger<ItemPriceUpdateEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _bus = bus;
            _logger = logger;
        }

        public async Task HandleAsync(ItemPriceUpdateEvent @event)
        {
            try
            {
                await _orderRepository.UpdatePrice(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError("System Exception",ex);
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }
    }
}