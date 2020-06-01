using System;
using System.Collections.Generic;
using OMF.Common.Models;
using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class OrderConfirmedEvent : Event
    {
        public int RestaurantId { get; }
        public IEnumerable<FoodOrderItem> OrderItems { get; }

        public OrderConfirmedEvent(int restaurantId, IEnumerable<FoodOrderItem> orderItems)
        {
            RestaurantId = restaurantId;
            OrderItems = orderItems;
        }
    }
}