using System;
using System.Collections.Generic;
using OMF.Common.Models;
using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class OrderConfirmedEvent : Event
    {
        public OrderConfirmedEvent(Guid id, int restaurantId, List<FoodOrderItem> orderItems, string address) : base(id)
        {
            RestaurantId = restaurantId;
            OrderItems = orderItems;
            Address = address;
        }

        public int RestaurantId { get; set; }
        public List<FoodOrderItem> OrderItems { get; set; }
        public string Address { get; set; }
    }
}