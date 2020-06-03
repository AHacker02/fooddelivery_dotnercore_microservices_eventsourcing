using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.Abstractions;
using OMF.RestaurantService.Query.Repository.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Query.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantManagementContext _database;
        private readonly IMapper _map;

        public RestaurantRepository(RestaurantManagementContext database, IMapper map)
        {
            _database = database;
            _map = map;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Restaurant>> SearchRestaurantAsync(string id, string name, string coordinateX,
            string coordinateY,
            string budget, string rating, string food, string distance, string cuisine)
        {
            //var filteredRestaurant = from r in _database.TblRestaurant.Include(x => x.TblLocation).Include(x => x.TblOffer).ThenInclude(y => y.TblMenu).Include(x => x.TblRestaurantDetails)
            //                         where (id == null || r.Id == Convert.ToInt32(id))
            //                               && (name == null || r.Name.ToLower().Contains(name.ToLower()))
            //                               && (budget == null || r.TblOffer.Average(x => x.Price) <= Convert.ToDecimal(budget))
            //                               && (food == null || r.TblOffer.Any(x => x.TblMenu.Item.ToLower().Contains(food.ToLower()) || food.ToLower().Contains(x.TblMenu.Item.ToLower())))
            //                               && (cuisine == null || r.TblOffer.Any(x => x.TblMenu.TblCuisine.Cuisine.ToLower().Contains(cuisine.ToLower()) || cuisine.Contains(x.TblMenu.TblCuisine.Cuisine.ToLower())))
            //                               && (rating == null || r.Rating >= Convert.ToDecimal(rating))
            //                               && (budget == null || r.Budget >= Convert.ToDecimal(budget))
            //                               && ((coordinateX == null && coordinateY == null) ||
            //                                   (r.TblLocation.Distance(Convert.ToDouble(coordinateX), Convert.ToDouble(coordinateY)) <= Convert.ToDouble(distance)))

            //                         orderby r.Rating descending
            //                         select r;

            var filteredRestaurant = _database.TblRestaurant
                .Include(x => x.TblLocation)
                .Include(x => x.TblOffer).ThenInclude(y => y.TblMenu)
                .Include(x => x.TblRestaurantDetails)
                .ToList()
                .Where(r =>
                    (id == null || r.Id == Convert.ToInt32(id))
                    && (name == null || r.Name.ToLower().Contains(name.ToLower()))
                    && (budget == null || r.TblOffer.Average(x => x.Price) <= Convert.ToDecimal(budget))
                    && (food == null || r.TblOffer.Any(x =>
                        x.TblMenu.Item.ToLower().Contains(food.ToLower()) ||
                        food.ToLower().Contains(x.TblMenu.Item.ToLower())))
                    && (cuisine == null || r.TblOffer.Any(x =>
                        x.TblMenu.TblCuisine.Cuisine.ToLower().Contains(cuisine.ToLower()) ||
                        cuisine.Contains(x.TblMenu.TblCuisine.Cuisine.ToLower())))
                    && (rating == null || r.Rating >= Convert.ToDecimal(rating))
                    && (budget == null || r.Budget <= Convert.ToDecimal(budget))
                    && ((coordinateX == null && coordinateY == null) ||
                        (r.TblLocation.Distance(Convert.ToDouble(coordinateX), Convert.ToDouble(coordinateY)) <=
                         Convert.ToDouble(distance)))
                )
                .OrderByDescending(r => r.Rating);


            return _map.Map<IEnumerable<Restaurant>>(filteredRestaurant);


        }
    }
}