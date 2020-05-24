using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.ReviewManagementService.Command.Repository.DataContext;

namespace OMF.ReviewManagementService.Command.Repository.Abstractions
{
    public interface IReviewRepository
    {
        Task UpsertReview(TblRating review);
        Task<IEnumerable<TblRating>> GetRestaurantReviews(int restaurantId);
    }
}