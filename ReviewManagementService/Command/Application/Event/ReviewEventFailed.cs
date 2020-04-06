using System;

namespace OMF.ReviewManagementService.Command.Application.Event
{
    public class ReviewEventFailed : ServiceBus.Abstractions.Event
    {
        public string Reason { get; }
        public string Code { get; }
        public Guid EventId { get; set; }
        public DateTime TimeStamp { get; set; }

        public ReviewEventFailed(string reason, string code, Guid eventId)
        {
            Reason = reason;
            Code = code;
            EventId = eventId;
            TimeStamp = DateTime.Now;
        }
    }
}
