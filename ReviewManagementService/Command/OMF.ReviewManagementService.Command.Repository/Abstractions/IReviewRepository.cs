using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.ReviewManagementService.Command.Repository.DataContext;

namespace OMF.ReviewManagementService.Command.Repository.Abstractions
{
    public interface IReviewRepository
    {
        /// <summary>
        /// Insert or update restaurant review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        Task UpsertReview(TblRating review);


        /// <summary>
        /// Get list of restaurant reviews
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        Task<IEnumerable<TblRating>> GetRestaurantReviews(int restaurantId);
    }
}