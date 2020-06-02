using System;

namespace OMF.OrderManagementService.Query.Repository.DataContext
{
    public class TblTableDetail
    {
        public int Id { get; set; }
        public int TblTableBookingId { get; set; }
        public int TableNo { get; set; }
        public decimal Price { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual TblTableBooking TblTableBooking { get; set; }
    }
}