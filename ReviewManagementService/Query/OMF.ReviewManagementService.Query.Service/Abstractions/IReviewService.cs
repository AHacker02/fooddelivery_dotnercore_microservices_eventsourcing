using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.ReviewManagementService.Query.Service.Abstractions
{
    public interface IReviewService
    {
        Task<IEnumerable<Rating>> GetRestaurantReviews(int restaurantId);
    }
}
