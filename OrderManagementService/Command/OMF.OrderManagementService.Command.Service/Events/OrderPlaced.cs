using System;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.Events
{
    public class PaymentInitiatedEvent : Event
    {
        public PaymentInitiatedEvent(int orderId,string domain)
        {
            OrderId = orderId;
            Domain = domain;
        }

        public int OrderId { get; set; }
        public string Domain { get; }
    }
}
