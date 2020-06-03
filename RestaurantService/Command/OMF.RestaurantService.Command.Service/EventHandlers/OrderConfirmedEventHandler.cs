using OMF.Common.Events;
using OMF.RestaurantService.Repository.Abstractions;
using ServiceBus.Abstractions;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OMF.RestaurantService.Command.Service.EventHandlers
{
    public class OrderConfirmedEventHandler : IEventHandler<OrderConfirmedEvent>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEventBus _bus;
        private readonly ILogger<OrderConfirmedEvent> _logger;

        public OrderConfirmedEventHandler(IRestaurantRepository restaurantRepository, IEventBus bus,ILogger<OrderConfirmedEvent> logger)
        {
            _restaurantRepository = restaurantRepository;
            _bus = bus;
            _logger = logger;
        }


        /// <summary>
        /// Handler Order confirmed event
        /// To update item stock after order
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleAsync(OrderConfirmedEvent @event)
        {
            try
            {
                foreach (var item in @event.OrderItems)
                {
                    var remainingStock = await _restaurantRepository.UpdateStockAsync(item.MenuId, Convert.ToInt32(item.Quantity));

                    _logger.LogInformation("Remaining stock {stock} in Restaurant {id}",remainingStock ,item.MenuId);
                    if (remainingStock == 0)
                    {
                        await _bus.PublishEvent(new ItemOutOfStockEvent(item.MenuId));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("System exception", ex);
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }
    }
}
