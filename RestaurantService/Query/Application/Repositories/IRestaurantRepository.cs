using OMF.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Query.Application.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    }
}
