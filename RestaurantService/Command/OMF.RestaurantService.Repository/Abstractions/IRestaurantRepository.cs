using OMF.RestaurantService.Query.Repository.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Repository.Abstractions
{
    public interface IRestaurantRepository
    {
        Task AddRestaurantsAsync(IEnumerable<TblRestaurant> restaurants);
        Task<int> UpdateStockAsync(int menuId, int quantity);
        Task<bool> UpdateRatingAsync(int restaurantId, decimal? rating);

        Task<bool> UpdatePriceAsync(int menuId, decimal price);
    }
}
