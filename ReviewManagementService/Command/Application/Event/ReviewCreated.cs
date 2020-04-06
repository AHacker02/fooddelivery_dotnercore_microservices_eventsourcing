using System;

namespace OMF.ReviewManagementService.Command.Application.Event
{
    public class ReviewCreated : ServiceBus.Abstractions.Event
    {
        public ReviewCreated(Guid restaurantId)
        {
            RestaurantId = restaurantId;
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.Now;
        }
        public Guid RestaurantId { get; set; }
        public Guid EventId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
