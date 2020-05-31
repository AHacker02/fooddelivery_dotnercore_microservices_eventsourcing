using System.Collections.Generic;

namespace OMF.Common.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public int Id { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string OpeningTime { get; set; }
        public string CloseTime { get; set; }
        public decimal Rating { get; set; }
        public decimal Budget { get; set; }
        
        public Location Location { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public IEnumerable<RestaurantDetails> RestaurantDetails { get; set; }
    }
}