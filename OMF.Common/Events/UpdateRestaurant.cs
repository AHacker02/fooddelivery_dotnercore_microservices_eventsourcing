using ServiceBus.Abstractions;
using System;

namespace OMF.Common.Events
{
    public class UpdateRestaurant : Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public string Location { get; set; }
        public string ListedCity { get; set; }
        public decimal ApproxCost { get; set; }
        public Guid EventId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
