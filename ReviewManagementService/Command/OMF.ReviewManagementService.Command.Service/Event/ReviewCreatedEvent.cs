using System;

namespace OMF.ReviewManagementService.Command.Service.Event
{
    public class ReviewCreatedEvent : ServiceBus.Abstractions.Event
    {
        public ReviewCreatedEvent(Guid restaurantId,Guid id):base(id)
        {
            RestaurantId = restaurantId;
        }
        public Guid RestaurantId { get; set; }
    }
}
