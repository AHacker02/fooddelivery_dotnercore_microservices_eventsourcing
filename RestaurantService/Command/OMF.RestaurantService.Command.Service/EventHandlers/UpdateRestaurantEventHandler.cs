using System;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Events;
using OMF.RestaurantService.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.RestaurantService.Command.Service.EventHandlers
{
    public class UpdateRestaurantEventHandler : IEventHandler<UpdateRestaurantEvent>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _map;
        private readonly IEventBus _bus;

        public UpdateRestaurantEventHandler(IRestaurantRepository restaurantRepository, IMapper map, IEventBus bus)
        {
            _restaurantRepository = restaurantRepository;
            _map = map;
            _bus = bus;
        }

        public async Task HandleAsync(UpdateRestaurantEvent @event)
        {
            try
            {
                await _restaurantRepository.UpdateRatingAsync(@event.RestaurantId, Convert.ToDecimal(@event.Rating));
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception", $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }
    }
}
