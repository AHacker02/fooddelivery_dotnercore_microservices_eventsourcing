using System;
using System.Collections.Generic;
using System.Text;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class TableBookingCommand:ServiceBus.Abstractions.Command
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
