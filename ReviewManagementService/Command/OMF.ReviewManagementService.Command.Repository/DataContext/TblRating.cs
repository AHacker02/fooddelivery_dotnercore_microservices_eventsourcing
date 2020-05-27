﻿using System;

namespace OMF.ReviewManagementService.Command.Repository.DataContext
{
    public class TblRating
    {
        public string Rating { get; set; }
        public string Comments { get; set; }
        public int TblRestaurantId { get; set; }
        public int Id { get; set; }
        public int TblCustomerId { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }
    }
}