using ServiceBus.Abstractions;
using System;

namespace OMF.OrderManagementService.Command.Application.Events
{
    public class PaymentInitiatedEvent : Event
    {
        public PaymentInitiatedEvent(Guid id) : base(id)
        {

        }
    }
}
