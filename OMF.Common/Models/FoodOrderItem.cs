﻿using System;

namespace OMF.Common.Models
{
    public class FoodOrderItem
    {
        public int Quantity { get; set; }
        public int MenuId { get; set; }
        public decimal Price { get; set; }
    }
}