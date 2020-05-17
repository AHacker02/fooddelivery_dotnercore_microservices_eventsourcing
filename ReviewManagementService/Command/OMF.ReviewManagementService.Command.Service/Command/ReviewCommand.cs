using System;

namespace OMF.ReviewManagementService.Command.Service.Command
{
    public class ReviewCommand : ServiceBus.Abstractions.Command
    {
        public Guid RestaurantId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewDescription { get; set; }
        public Guid UserId { get; set; }
    }
}
