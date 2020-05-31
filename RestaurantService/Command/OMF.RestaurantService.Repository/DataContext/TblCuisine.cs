using System;
using System.Collections.Generic;

namespace OMF.RestaurantService.Query.Repository.DataContext
{
    public partial class TblCuisine
    {
        public TblCuisine()
        {
            TblMenu = new HashSet<TblMenu>();
        }

        public string Cuisine { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public ICollection<TblMenu> TblMenu { get; set; }
    }
}
