using System;

namespace OMF.RestaurantService.Query.Repository.DataContext
{
    public partial class TblRestaurantDetails
    {
        public int TblRestaurantId { get; set; }
        public int TableCount { get; set; }
        public int TableCapacity { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime ModifiedTimeStamp { get; set; }
        public DateTime CreatedRecordTimeStamp { get; set; }

        public TblRestaurant TblRestaurant { get; set; }
    }
}
