using OMF.Common.Models;
using System;
using System.Collections.Generic;

namespace OMF.OrderManagementService.Command.Application.Commands
{
    public class OrderCommand : ServiceBus.Abstractions.Command
    {
        public Guid RestaurantId { get; set; }
        public List<Item> OrderItems { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
    }

}
