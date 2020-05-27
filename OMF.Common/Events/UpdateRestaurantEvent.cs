using System;
using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class UpdateRestaurantEvent : Event
    {
        public UpdateRestaurantEvent(int restaurantId, string rating)
        {
            RestaurantId = restaurantId;
            Rating = rating;
        }

        public int RestaurantId { get; set; }
        public string Rating { get; set; }
    }
}