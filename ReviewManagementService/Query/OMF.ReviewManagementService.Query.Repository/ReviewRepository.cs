using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Abstractions;
using OMF.Common.Models;
using OMF.ReviewManagementService.Query.Repository.Abstractions;

namespace OMF.ReviewManagementService.Query.Repository
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
    }
}
