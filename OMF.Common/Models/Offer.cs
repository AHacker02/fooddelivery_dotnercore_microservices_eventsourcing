using System;

namespace OMF.Common.Models
{
    public class Offer
    {
        public decimal Price { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Discount { get; set; }
        public int Id { get; set; }
    }
}