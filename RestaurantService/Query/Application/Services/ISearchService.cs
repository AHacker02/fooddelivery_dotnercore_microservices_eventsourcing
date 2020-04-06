using OMF.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Query.Application.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<Restaurant>> SearchRestaurant(string id, string name, string location, string budget,
            string rating, string food);
    }
}
