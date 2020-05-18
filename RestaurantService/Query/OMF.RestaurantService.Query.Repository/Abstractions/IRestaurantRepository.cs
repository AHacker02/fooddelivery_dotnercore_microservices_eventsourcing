using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.DataContext;

namespace OMF.RestaurantService.Query.Repository.Abstractions
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<IEnumerable<Restaurant>> SearchRestaurantAsync(int Id, string name, string coordinateX, string coordinateY,
            string budget, string rating, string food, string distance, string cuisine);
    }
}