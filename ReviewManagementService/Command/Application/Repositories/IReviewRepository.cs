using OMF.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.ReviewManagementService.Command.Application.Repositories
{
    public interface IReviewRepository
    {
        Task UpsertReview(Review review);
        Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId);
    }
}
