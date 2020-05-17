using System;

namespace OMF.Common.Models
{
    public class Review
    {
        public Review()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewDescription { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid UserId { get; set; }
    }
}