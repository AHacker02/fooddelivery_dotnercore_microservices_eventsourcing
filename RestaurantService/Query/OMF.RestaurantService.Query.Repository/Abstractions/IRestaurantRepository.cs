using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.RestaurantService.Query.Repository.Abstractions
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    }
}