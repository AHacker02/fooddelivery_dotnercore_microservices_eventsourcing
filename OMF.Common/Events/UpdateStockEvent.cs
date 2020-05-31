using System;
using System.Collections.Generic;
using System.Text;
using OMF.Common.Models;
using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class UpdateStockEvent:Event
    {
        public int RestaurantId { get; }
        public IEnumerable<FoodOrderItem> OrderItems { get; }

        public UpdateStockEvent(int restaurantId, IEnumerable<FoodOrderItem> orderItems)
        {
            RestaurantId = restaurantId;
            OrderItems = orderItems;
        }
    }
}
