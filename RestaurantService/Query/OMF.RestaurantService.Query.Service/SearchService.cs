using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.Abstractions;
using OMF.RestaurantService.Query.Service.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Query.Service
{
    public class SearchService : ISearchService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public SearchService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<Restaurant>> SearchRestaurant(string id, string name, string coordinateX,
            string coordinateY,
            string budget, string rating, string food, string distance, string cuisine)
            => await _restaurantRepository.SearchRestaurantAsync(id, name, coordinateX, coordinateY, budget, rating, food, distance, cuisine);


    }
}