using OMF.RestaurantService.Query.Repository.DataContext;
using OMF.RestaurantService.Repository.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantManagementContext _database;


        public RestaurantRepository(RestaurantManagementContext database)
        {
            _database = database;
        }
        public async Task AddRestaurantsAsync(IEnumerable<TblRestaurant> restaurants)
            => await _database.AddRangeAsync(restaurants);

        public async Task<int> UpdateStockAsync(int menuId, int quantity)
        {

            var item = _database.TblMenu.FirstOrDefault(x => x.Id == menuId);
            item.Quantity -= quantity;

            await _database.SaveChangesAsync();
            return item.Quantity;
        }

        public async Task<bool> UpdateRatingAsync(int restaurantId, decimal? rating)
        {
            var item = _database.TblRestaurant.FirstOrDefault(x => x.Id == restaurantId);

            if (item == null)
                return false;

            item.Rating = rating;

            return await _database.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePriceAsync(int menuId, decimal price)
        {
            var item = _database.TblOffer.FirstOrDefault(x => x.TblMenuId == menuId);

            if (item == null)
                return false;

            item.Price = price;
            return await _database.SaveChangesAsync() > 0;
        }
    }
}
