using System;
using System.Linq;
using System.Threading.Tasks;
using OMF.Common.Events;
using OMF.RestaurantService.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.RestaurantService.Command.Service.EventHandlers
{
    public class OrderConfirmedEventHandler : IEventHandler<OrderConfirmedEvent>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEventBus _bus;

        public OrderConfirmedEventHandler(IRestaurantRepository restaurantRepository, IEventBus bus)
        {
            _restaurantRepository = restaurantRepository;
            _bus = bus;
        }
        public async Task HandleAsync(OrderConfirmedEvent @event)
        {
            try
            {
                foreach (var item in @event.OrderItems)
                {
                   var remainingStock= await _restaurantRepository.UpdateStockAsync(item.MenuId,item.Quantity);
                   if (remainingStock == 0)
                   {
                       await _bus.PublishEvent(new ItemOutOfStockEvent(item.MenuId));
                   }
                }
                
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }
    }
}
