using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.RestaurantService.Query.Service.Abstractions
{
    public interface ISearchService
    {
        Task<IEnumerable<Restaurant>> SearchRestaurant(string id, string name, string location, string budget,
            string rating, string food);
    }
}