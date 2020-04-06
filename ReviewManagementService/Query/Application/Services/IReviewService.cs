using OMF.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.ReviewManagementService.Query.Application.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId);
    }
}
