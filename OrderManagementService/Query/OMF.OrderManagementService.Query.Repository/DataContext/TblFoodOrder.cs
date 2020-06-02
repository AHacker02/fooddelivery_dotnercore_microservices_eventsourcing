using System;
using System.Collections.Generic;
using OMF.OrderManagementService.Query.Repository.Abstractions;

namespace OMF.OrderManagementService.Query.Repository.DataContext
{
    public class TblFoodOrder:IEntity
    {
        public TblFoodOrder()
        {
            TblFoodOrderItem=new HashSet<TblFoodOrderItem>();
        }
        public int Id { get; set; }
        public int TblCustomerId { get; set; }
        public int TblRestaurantId { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<TblFoodOrderItem> TblFoodOrderItem { get; set; }
       public string Status { get ; set ; }
       public int PaymentId { get; set; }
    }
}