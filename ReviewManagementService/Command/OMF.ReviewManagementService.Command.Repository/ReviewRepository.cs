using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Abstractions;
using OMF.Common.Models;
using OMF.ReviewManagementService.Command.Repository.Abstractions;

namespace OMF.ReviewManagementService.Command.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly INoSqlDataAccess _database;

        public ReviewRepository(INoSqlDataAccess database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId)
            => await _database.Find<Review>(x => x.RestaurantId == restaurantId);

        public async Task UpsertReview(Review review)
        {
            var existingReview = await _database.Single<Review>(x => x.UserId == review.UserId && x.RestaurantId == review.RestaurantId);
            if (existingReview != null)
            {
                await _database.Delete<Review>(x => x.Id == review.Id);

            }
            await _database.Add(review);
        }
    }
}
