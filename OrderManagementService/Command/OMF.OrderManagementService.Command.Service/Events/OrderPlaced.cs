using System;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.Events
{
    public class PaymentInitiatedEvent : Event
    {
        public PaymentInitiatedEvent(Guid id) : base(id)
        {

        }
    }
}
