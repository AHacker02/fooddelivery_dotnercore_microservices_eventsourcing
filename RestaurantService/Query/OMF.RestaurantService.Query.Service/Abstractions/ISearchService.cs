using OMF.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Query.Service.Abstractions
{
    public interface ISearchService
    {
        /// <summary>
        /// Search retaurant on multiple parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="coordinateX"></param>
        /// <param name="coordinateY"></param>
        /// <param name="budget"></param>
        /// <param name="rating"></param>
        /// <param name="food"></param>
        /// <param name="distance"></param>
        /// <param name="cuisine"></param>
        /// <returns>List od restaurants</returns>
        Task<IEnumerable<Restaurant>> SearchRestaurant(string id, string name, string coordinateX, string coordinateY,
            string budget, string rating, string food, string distance, string cuisine);
    }
}