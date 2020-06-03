using AutoMapper;
using OMF.Common.Events;
using OMF.RestaurantService.Repository.Abstractions;
using ServiceBus.Abstractions;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OMF.RestaurantService.Command.Service.EventHandlers
{
    public class UpdateRestaurantEventHandler : IEventHandler<UpdateRestaurantEvent>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _map;
        private readonly IEventBus _bus;
        private readonly ILogger<UpdateRestaurantEventHandler> _logger;

        public UpdateRestaurantEventHandler(IRestaurantRepository restaurantRepository, IMapper map, IEventBus bus,ILogger<UpdateRestaurantEventHandler> logger)
        {
            _restaurantRepository = restaurantRepository;
            _map = map;
            _bus = bus;
            _logger = logger;
        }


        /// <summary>
        /// Handler Update restaurant event
        /// To update restaurant rating 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleAsync(UpdateRestaurantEvent @event)
        {
            try
            {
                await _restaurantRepository.UpdateRatingAsync(@event.RestaurantId, Convert.ToDecimal(@event.Rating));
                _logger.LogInformation("Restaurant {id} rating updated to {rating}",@event.RestaurantId,@event.Rating);
            }
            catch (Exception ex)
            {
                _logger.LogError("System exception",ex);
                await _bus.PublishEvent(new ExceptionEvent("system_exception", $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }
    }
}
