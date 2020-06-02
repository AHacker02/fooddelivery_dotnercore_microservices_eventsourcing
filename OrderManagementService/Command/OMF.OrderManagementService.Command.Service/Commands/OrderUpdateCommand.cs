using System.Collections.Generic;
using MediatR;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class OrderUpdateCommand : IRequest<Response>
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public List<FoodOrderItem> OrderItems { get; set; }
    }
}