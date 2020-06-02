using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.ReviewManagementService.Query.Repository.Abstractions
{
    public interface IReviewRepository
    {
        /// <summary>
        /// Restaurant review by Id
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns>List of reviews by Id</returns>
        Task<IEnumerable<Rating>> GetRestaurantReviews(int restaurantId);
    }
}
