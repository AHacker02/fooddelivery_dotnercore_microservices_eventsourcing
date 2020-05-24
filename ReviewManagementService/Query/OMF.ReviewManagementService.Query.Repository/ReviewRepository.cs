using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Models;
using OMF.ReviewManagementService.Query.Repository.Abstractions;
using OMF.ReviewManagementService.Query.Repository.DataContext;

namespace OMF.ReviewManagementService.Query.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RatingDataContext _database;
        private readonly IMapper _map;

        public ReviewRepository(RatingDataContext database,IMapper map)
        {
            _database = database;
            _map = map;
        }

        public async Task<IEnumerable<Rating>> GetRestaurantReviews(int restaurantId)
            => _map.Map<IEnumerable<Rating>>(_database.TblRating.Where(x => x.TblRestaurantId == restaurantId));
    }
}
