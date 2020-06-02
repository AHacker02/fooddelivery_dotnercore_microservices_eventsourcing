using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.ReviewManagementService.Query.Service.Abstractions
{
    public interface IReviewService
    {
        /// <summary>
        /// Restaurant review by Id
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns>List of reviews by Id</returns>
        Task<IEnumerable<Rating>> GetRestaurantReviews(int restaurantId);
    }
}
