using System;
using System.Collections.Generic;

namespace OMF.RestaurantService.Query.Repository.DataContext
{
    public partial class TblRestaurant
    {
        public TblRestaurant()
        {
            TblOffer = new HashSet<TblOffer>();
            TblRestaurantDetails = new HashSet<TblRestaurantDetails>();
        }

        public string Name { get; set; }
        public int TblLocationId { get; set; }
        public string ContactNo { get; set; }
        public int Id { get; set; }
        public DateTime ModifiedTimeStamp { get; set; }
        public DateTime CreatedRecordTimeStamp { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string OpeningTime { get; set; }
        public string CloseTime { get; set; }

        public TblLocation TblLocation { get; set; }
        public ICollection<TblOffer> TblOffer { get; set; }
        public ICollection<TblRestaurantDetails> TblRestaurantDetails { get; set; }
        public decimal? Rating { get; set; }
        public decimal? Budget { get; set; }
    }
}
