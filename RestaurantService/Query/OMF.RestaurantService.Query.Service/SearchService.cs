using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.Abstractions;
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

        public async Task<IEnumerable<Restaurant>> SearchRestaurant(string id, string name, string location,
            string budget, string rating, string food)
        {
            var restaurants = await _restaurantRepository.GetAllRestaurantsAsync();

            var filteredRestaurant = from r in restaurants
                where (id == null || r.Id == Guid.Parse(id))
                      && (name == null || r.Name.ToLower().Contains(name.ToLower()))
                      && (location == null || r.Location.ToLower().Contains(location.ToLower()) ||
                          r.ListedCity.ToLower().Contains(location.ToLower()) ||
                          location.ToLower().Contains(r.Location.ToLower()) ||
                          location.ToLower().Contains(r.ListedCity.ToLower()))
                      && (budget == null || r.ApproxCost >= Convert.ToDecimal(budget))
                      && (rating == null || Convert.ToDecimal(r.Rating) <= Convert.ToDecimal(rating))
                      && (food == null || r.Menu.Any(x =>
                          x.Item.ToLower().Contains(food.ToLower()) || food.ToLower().Contains(x.Item.ToLower())))
                select r;

            return filteredRestaurant;
        }
    }
}