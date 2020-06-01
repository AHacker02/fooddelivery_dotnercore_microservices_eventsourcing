using System;
using System.Collections.Generic;
using System.Linq;

namespace OMF.Common.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<FoodOrderItem> OrderItems { get; set; }
        public string Status { get ; set ; }
        public int PaymentId { get; set; }
    }
}