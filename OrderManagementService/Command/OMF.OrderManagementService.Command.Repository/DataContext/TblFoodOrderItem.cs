﻿using System;
using OMF.OrderManagementService.Command.Repository.Abstractions;

namespace OMF.OrderManagementService.Command.Repository.DataContext
{
    public class TblFoodOrderItem : IEntity
    {
        public int TblFoodOrderId { get; set; }
        public int TblMenuId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual TblFoodOrder TblFoodOrder { get; set; }
        public int Id { get; set; }
    }
}