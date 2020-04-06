using OMF.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.ReviewManagementService.Query.Application.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId);
    }
}
