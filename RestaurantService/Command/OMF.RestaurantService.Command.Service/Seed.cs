using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.DataContext;
using OMF.RestaurantService.Repository.Abstractions;

namespace OMF.RestaurantService.Command.Service
{
    public class Seed
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly RestaurantManagementContext _database;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public Seed(IRestaurantRepository restaurantRepository,RestaurantManagementContext database,HttpClient client,IConfiguration configuration)
        {
            _restaurantRepository = restaurantRepository;
            _database = database;
            _client = client;
            _configuration = configuration;
        }

        public void SeedRestaurants()
        {
            var restaurant = _database.TblRestaurant.Where(x => x.Rating == null || x.Budget==null);
            foreach (var rest in restaurant)
            {
                var httpResponse = _client.GetAsync(new Uri(string.Format(_configuration["RestaurantURL"],rest.Id)));
            }
        }
    }
}
