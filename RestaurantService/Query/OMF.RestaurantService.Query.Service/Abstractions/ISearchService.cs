using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.DataContext;

namespace OMF.RestaurantService.Query.Service.Abstractions
{
    public interface ISearchService
    {
        Task<IEnumerable<Restaurant>> SearchRestaurant(string id, string name, string coordinateX, string coordinateY,
            string budget, string rating, string food, string distance, string cuisine);
    }
}