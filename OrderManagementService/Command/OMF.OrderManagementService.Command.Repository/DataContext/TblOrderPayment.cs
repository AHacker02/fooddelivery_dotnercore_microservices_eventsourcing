using System;
using OMF.OrderManagementService.Command.Repository.Abstractions;

namespace OMF.OrderManagementService.Command.Repository.DataContext
{
    public class TblOrderPayment : IEntity
    {
        public Guid TransactionId { get; set; }
        public string Remarks { get; set; }
        public int TblCustomerId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Id { get; set; }
    }
}