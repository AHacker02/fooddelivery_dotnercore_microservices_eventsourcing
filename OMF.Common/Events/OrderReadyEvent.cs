using System;
using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class OrderReadyEvent : Event
    {
        public OrderReadyEvent(string fromAddress, string address, Guid id) : base(id)
        {
            FromAddress = fromAddress;
            ToAddress = address;
        }

        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
    }
}