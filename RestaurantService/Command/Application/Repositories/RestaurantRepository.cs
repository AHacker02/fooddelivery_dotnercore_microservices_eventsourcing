using DataAccess.Abstractions;
using DataAccess.MongoDb;
using OMF.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Command.Application.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private INoSqlDataAccess _database;

        public RestaurantRepository(IConnectionFactory connection)
        {
            _database = new MongoDbDataAccess(connection, "Restaurants");
        }
        public async Task AddRestaurantsAsync(IEnumerable<Restaurant> restaurants)
            => await _database.AddRangeAsync(restaurants);


        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
            => (await _database.All<Restaurant>()).AsEnumerable();

        public async Task UpdateStock(List<Item> orderItems)
        {
            var restaurant = await _database.Single<Restaurant>(x
                => x.Menu.Any(m => m.Id == orderItems.FirstOrDefault().FoodId));
            foreach (var item in orderItems)
            {
                restaurant.Menu.FirstOrDefault(x => x.Id == item.FoodId).Quantity -= item.Quantity;
            }

            await _database.Delete<Restaurant>(x => x.Id == restaurant.Id);
            await _database.Add(restaurant);
        }
    }
}
