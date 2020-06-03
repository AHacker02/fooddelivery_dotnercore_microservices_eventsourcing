using OMF.Common.Models;
using ServiceBus.Abstractions;
using System.Collections.Generic;

namespace OMF.Common.Events
{
    public class OrderConfirmedEvent : Event
    {
        public OrderConfirmedEvent(int restaurantId, IEnumerable<FoodOrderItem> orderItems)
        {
            RestaurantId = restaurantId;
            OrderItems = orderItems;
        }

        public int RestaurantId { get; }
        public IEnumerable<FoodOrderItem> OrderItems { get; }
    }
}