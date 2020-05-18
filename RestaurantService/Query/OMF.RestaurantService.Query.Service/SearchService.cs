using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.Abstractions;
using OMF.RestaurantService.Query.Repository.DataContext;
using OMF.RestaurantService.Query.Service.Abstractions;

namespace OMF.RestaurantService.Query.Service
{
    public class SearchService : ISearchService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public SearchService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<Restaurant>> SearchRestaurant(int Id , string name , string coordinateX , string coordinateY ,
            string budget , string rating , string food , string distance, string cuisine)
            => await _restaurantRepository.SearchRestaurantAsync(Id, name, coordinateX, coordinateY, budget, rating, food, distance,cuisine);
        
        
    }
}