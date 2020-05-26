using System;
using System.Collections.Generic;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class OrderCommand : ServiceBus.Abstractions.Command
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public string Address { get; set; }
        public List<FoodOrderItem> OrderItems { get; set; }
        public bool BookNow { get; set; }
    }

}
