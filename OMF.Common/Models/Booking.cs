using System;
using System.Collections.Generic;

namespace OMF.Common.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public int PaymentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<TableDetail> TableDetail { get; set; }
    }

    public class TableDetail
    {
        public int TableNo { get; set; }
        public decimal Price { get; set; }
    }
}