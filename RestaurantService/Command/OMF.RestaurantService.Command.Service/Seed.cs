using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OMF.Common.Models;
using OMF.RestaurantService.Repository.Abstractions;

namespace OMF.RestaurantService.Command.Service
{
    public class Seed
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public Seed(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void SeedRestaurants()
        {
            var restaurants = _restaurantRepository.GetAllRestaurantsAsync().GetAwaiter().GetResult();
            if (!restaurants.Any())
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Zomato.json";
                string mockData = File.ReadAllText(filePath);
                var data = (JsonConvert.DeserializeObject<IEnumerable<Restaurant>>(mockData)).ToList();
                data.ForEach(x =>
                {
                    x.Menu.ForEach(m => m.Quantity = 150);
                    x.Rating = (string.IsNullOrEmpty(x.Rating) || x.Rating == "NEW") ? "0" : x.Rating.Replace("/5", "");
                });
                _restaurantRepository.AddRestaurantsAsync(data).GetAwaiter();
            }
        }
    }
}
