using System;

namespace OMF.Common.Models
{
    public class Item
    {
        public int Quantity { get; set; }
        public Guid FoodId { get; set; }
        public decimal Price { get; set; }
    }
}