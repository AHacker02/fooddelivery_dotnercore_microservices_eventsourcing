using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class TableBookingCommand:IRequest<Response>
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int MemberCount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
