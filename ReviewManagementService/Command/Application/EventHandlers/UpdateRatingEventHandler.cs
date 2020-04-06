using Microsoft.Extensions.Configuration;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.ReviewManagementService.Command.Application.Event;
using OMF.ReviewManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OMF.ReviewManagementService.Command.Application.EventHandlers
{
    public class UpdateRatingEventHandler : IEventHandler<ReviewCreatedEvent>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IConfiguration _configuration;
        private readonly IEventBus _bus;

        public UpdateRatingEventHandler(IReviewRepository reviewRepository, IConfiguration configuration, IEventBus bus)
        {
            _reviewRepository = reviewRepository;
            _configuration = configuration;
            _bus = bus;
        }
        public async Task HandleAsync(ReviewCreatedEvent @event)
        {
            try
            {
                Restaurant restaurant = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["RestaurantUrl"]);
                    var eventUser = new User()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Event"
                    };
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", eventUser.GenerateJwtToken(_configuration["Token"]));
                    var response = await client.GetAsync($"api/restaurant?Id={@event.RestaurantId}");
                    if (response.IsSuccessStatusCode)
                    {
                        restaurant = (await response.Content.ReadAsAsync<IEnumerable<Restaurant>>()).FirstOrDefault();
                    }
                }

                var currentRating = string.IsNullOrEmpty(restaurant.Rating) ? 0 : restaurant.Rating.Contains("/5") ?
                     Convert.ToDecimal(restaurant.Rating.Replace("/5", "")) : 0;

                var restaurantReviews = await _reviewRepository.GetRestaurantReviews(@event.RestaurantId);

                var newRating = (currentRating + restaurantReviews.Sum(x => x.Rating) / restaurantReviews.Count() + 1);

                await _bus.PublishEvent(new UpdateRestaurantEvent(@event.Id)
                {
                    RestaurantId = @event.RestaurantId,
                    Rating = newRating.ToString()
                });
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception", $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }

        }
    }
}
