using System;
using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class UpdateRestaurantEvent : Event
    {
        public UpdateRestaurantEvent(Guid id) : base(id)
        {
        }

        public Guid RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public string Location { get; set; }
        public string ListedCity { get; set; }
        public decimal ApproxCost { get; set; }
    }
}