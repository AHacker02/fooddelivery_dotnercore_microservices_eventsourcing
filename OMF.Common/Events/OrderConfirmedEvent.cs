using OMF.Common.Models;
using ServiceBus.Abstractions;
using System;
using System.Collections.Generic;

namespace OMF.Common.Events
{
    public class OrderConfirmedEvent : Event
    {
        public OrderConfirmedEvent(Guid id, Guid restaurantId, List<Item> orderItems, string address) : base(id)
        {
            RestaurantId = restaurantId;
            OrderItems = orderItems;
            Address = address;
        }
        public Guid RestaurantId { get; set; }
        public List<Item> OrderItems { get; set; }
        public string Address { get; set; }
    }
}
