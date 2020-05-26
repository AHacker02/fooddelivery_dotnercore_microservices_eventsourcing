using System;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.Events
{
    public class PaymentInitiatedEvent : Event
    {
        public PaymentInitiatedEvent(Guid id,int orderId,string domain) : base(id)
        {
            OrderId = orderId;
            Domain = domain;
        }

        public int OrderId { get; set; }
        public string Domain { get; }
    }
}
