using OMF.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Query.Repository.Abstractions
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();


        /// <summary>
        /// Search restaurant on multiple parameters
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
        /// <returns>:ist of restaurants ordered by rating</returns>
        Task<IEnumerable<Restaurant>> SearchRestaurantAsync(string id, string name, string coordinateX,
            string coordinateY,
            string budget, string rating, string food, string distance, string cuisine);
    }
}