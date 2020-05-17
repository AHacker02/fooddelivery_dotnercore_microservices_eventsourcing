using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Abstractions;
using DataAccess.MongoDb;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.Abstractions;

namespace OMF.RestaurantService.Query.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly INoSqlDataAccess _database;

        public RestaurantRepository(IConnectionFactory connection)
        {
            _database = new MongoDbDataAccess(connection, "Restaurants");
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return (await _database.All<Restaurant>()).AsEnumerable();
        }
    }
}