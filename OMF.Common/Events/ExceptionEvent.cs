using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class ExceptionEvent : Event
    {
        public ExceptionEvent(string code, string message, dynamic eventData)
        {
            Code = code;
            Message = message;
            EventData = eventData;
        }
        public string Code { get; set; }
        public string Message { get; set; }
        public dynamic EventData { get; set; }
    }
}
