using System;
using System.Collections.Generic;
using System.Linq;

namespace OMF.Common.Models
{
    public class Order
    {
        public DateTime TimeStamp { get; set; }
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public List<FoodOrderItem> OrderItems { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public decimal TotalAmmount => OrderItems.Sum(x => x.Quantity * x.Price);
    }
}