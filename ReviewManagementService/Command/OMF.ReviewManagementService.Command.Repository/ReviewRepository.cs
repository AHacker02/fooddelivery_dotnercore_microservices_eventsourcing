using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMF.ReviewManagementService.Command.Repository.Abstractions;
using OMF.ReviewManagementService.Command.Repository.DataContext;

namespace OMF.ReviewManagementService.Command.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RatingDataContext _database;

        public ReviewRepository(RatingDataContext database)
        {
            _database = database;
        }

        public async Task<IEnumerable<TblRating>> GetRestaurantReviews(int restaurantId)
        {
            return _database.TblRating.Where(x => x.TblRestaurantId == restaurantId);
        }

        

        public async Task UpsertReview(TblRating review)
        {
            var existingReview = _database.TblRating.FirstOrDefault(x =>
                x.TblCustomerId == review.TblCustomerId && x.TblRestaurantId == review.TblRestaurantId);

            if (existingReview != null) _database.Remove(existingReview);

            review.RecordTimeStamp = review.RecordTimeStampCreated = DateTime.Now;

            _database.Add(review);
            await _database.SaveChangesAsync();
        }
    }
}