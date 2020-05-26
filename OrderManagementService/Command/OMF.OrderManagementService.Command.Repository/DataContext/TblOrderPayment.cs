using System;

namespace OMF.OrderManagementService.Command.Repository.DataContext
{
    public class TblOrderPayment
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public string Remarks { get; set; }
        public int TblCustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
        public string Domain { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}