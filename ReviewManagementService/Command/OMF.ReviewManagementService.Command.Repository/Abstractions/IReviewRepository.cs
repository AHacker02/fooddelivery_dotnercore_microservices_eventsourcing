using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.ReviewManagementService.Command.Repository.Abstractions
{
    public interface IReviewRepository
    {
        Task UpsertReview(Review review);
        Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId);
    }
}
