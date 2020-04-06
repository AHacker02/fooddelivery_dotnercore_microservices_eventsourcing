using OMF.Common.Models;
using OMF.ReviewManagementService.Query.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.ReviewManagementService.Query.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId)
            => await _reviewRepository.GetRestaurantReviews(restaurantId);
    }
}
