using System;
using System.Collections.Generic;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class OrderCommand : ServiceBus.Abstractions.Command
    {
        public Guid RestaurantId { get; set; }
        public List<Item> OrderItems { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
    }

}
