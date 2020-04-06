using DataAccess.Abstractions;
using DataAccess.MongoDb;
using OMF.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Query.Application.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {

        private INoSqlDataAccess _database;

        public RestaurantRepository(IConnectionFactory connection)
        {
            _database = new MongoDbDataAccess(connection, "Restaurants");
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
            => (await _database.All<Restaurant>()).AsEnumerable();
    }
}
