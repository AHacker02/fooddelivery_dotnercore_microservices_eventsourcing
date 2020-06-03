using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.EventHandlers
{
    public class ItemOutOfStockEventHandler : IEventHandler<ItemOutOfStockEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _bus;
        private readonly ILogger<ItemOutOfStockEventHandler> _logger;

        public ItemOutOfStockEventHandler(IOrderRepository orderRepository,IEventBus bus, ILogger<ItemOutOfStockEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _bus = bus;
            _logger = logger;
        }

        public async Task HandleAsync(ItemOutOfStockEvent @event)
        {
            try
            {
                await _orderRepository.OrderOutOfStock(@event);
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