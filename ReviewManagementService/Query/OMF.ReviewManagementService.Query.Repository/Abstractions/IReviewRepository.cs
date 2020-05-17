using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.ReviewManagementService.Query.Repository.Abstractions
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId);
    }
}
