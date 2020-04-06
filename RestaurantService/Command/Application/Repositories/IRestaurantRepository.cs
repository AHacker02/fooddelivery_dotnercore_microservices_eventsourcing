using OMF.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Command.Application.Repositories
{
    public interface IRestaurantRepository
    {
        Task AddRestaurantsAsync(IEnumerable<Restaurant> restaurants);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task UpdateStock(List<Item> orderItems);
    }
}
