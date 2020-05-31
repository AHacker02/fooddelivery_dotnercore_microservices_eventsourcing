﻿using System;
using System.Collections.Generic;

namespace OMF.RestaurantService.Query.Repository.DataContext
{
    public partial class TblMenu
    {
        public TblMenu()
        {
            TblOffer = new HashSet<TblOffer>();
        }

        public string Item { get; set; }
        public int TblCuisineId { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public TblCuisine TblCuisine { get; set; }
        public ICollection<TblOffer> TblOffer { get; set; }
        public int Quantity { get; set; }
    }
}
