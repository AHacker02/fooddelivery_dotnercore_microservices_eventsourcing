using System;
using System.Collections.Generic;
using System.Linq;
using OMF.OrderManagementService.Command.Repository.Abstractions;

namespace OMF.OrderManagementService.Command.Repository.DataContext
{
    public class TblTableBooking:IPaymentEntity
    {
        public TblTableBooking()
        {
            TblTableDetail=new HashSet<TblTableDetail>();
        }
        public int Id { get; set; }
        public int TblCustomerId { get; set; }
        public int TblRestaurantId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<TblTableDetail> TblTableDetail { get; set; }
       
    }
}