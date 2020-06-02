using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class OrderCommand : IRequest<Response>
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public string Address { get; set; }
        public List<FoodOrderItem> OrderItems { get; set; }
        public bool BookNow { get; set; }
    }
}