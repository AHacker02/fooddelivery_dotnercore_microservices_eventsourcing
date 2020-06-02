using System;
using System.Collections.Generic;
using OMF.OrderManagementService.Command.Repository.Abstractions;

namespace OMF.OrderManagementService.Command.Repository.DataContext
{
    public class TblTableBooking : IEntity
    {
        public TblTableBooking()
        {
            TblTableDetail = new HashSet<TblTableDetail>();
        }

        public int TblCustomerId { get; set; }
        public int TblRestaurantId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public int PaymentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<TblTableDetail> TblTableDetail { get; set; }
        public int Id { get; set; }
    }
}