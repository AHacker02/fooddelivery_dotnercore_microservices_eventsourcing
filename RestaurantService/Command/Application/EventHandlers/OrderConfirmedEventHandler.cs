using OMF.Common.Events;
using OMF.RestaurantService.Command.Application.Repositories;
using ServiceBus.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Command.Application.EventHandlers
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
                await _restaurantRepository.UpdateStock(@event.OrderItems);
                var restaurant = (await _restaurantRepository.GetAllRestaurantsAsync()).FirstOrDefault(x => x.Id == @event.RestaurantId);

                await _bus.PublishEvent(new OrderReadyEvent(restaurant.Address, @event.Address, @event.Id));
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }
    }
}
