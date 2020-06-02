using System;
using OMF.OrderManagementService.Query.Repository.Abstractions;

namespace OMF.OrderManagementService.Query.Repository.DataContext
{
    public class TblOrderPayment: IEntity
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public string Remarks { get; set; }
        public int TblCustomerId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}