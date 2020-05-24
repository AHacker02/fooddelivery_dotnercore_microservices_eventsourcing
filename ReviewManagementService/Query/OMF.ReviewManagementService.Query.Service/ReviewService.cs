using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.ReviewManagementService.Query.Repository.Abstractions;
using OMF.ReviewManagementService.Query.Service.Abstractions;

namespace OMF.ReviewManagementService.Query.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Rating>> GetRestaurantReviews(int restaurantId)
            => await _reviewRepository.GetRestaurantReviews(restaurantId);
    }
}
