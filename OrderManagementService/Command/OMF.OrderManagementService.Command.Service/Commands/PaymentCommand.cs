namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class PaymentCommand:ServiceBus.Abstractions.Command
    {
        public string Remarks { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Domain { get; set; }
        public string PaymentType { get; set; }
    }
}