using System;

namespace OMF.Common.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public string Remarks { get; set; }
        public int CustomerId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
    }
}