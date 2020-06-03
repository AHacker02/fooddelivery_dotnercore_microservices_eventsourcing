using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.DataContext;
using OMF.RestaurantService.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace OMF.RestaurantService.Command.Service
{
    public class Seed
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly RestaurantManagementContext _database;
        private readonly IConfiguration _configuration;

        public Seed(IRestaurantRepository restaurantRepository, RestaurantManagementContext database, IConfiguration configuration)
        {
            _restaurantRepository = restaurantRepository;
            _database = database;
            _configuration = configuration;
        }

        public void SeedRestaurants()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var _client = new HttpClient())
            {

                var user = new User() { Id = 0, Email = "System" };
                _client.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " + user.GenerateJwtToken(_configuration["Token"]));

                var restaurant = _database.TblRestaurant.Include(x=>x.TblOffer).Where(x => x.Rating == 0 || x.Budget == 0).Include(x => x.TblOffer);
                foreach (var rest in restaurant)
                {

                    var httpResponse = _client
                        .GetAsync(new Uri(string.Format(_configuration["RestaurantURL"], rest.Id))).GetAwaiter()
                        .GetResult();

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var response = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var restaurantRating = JsonConvert.DeserializeObject<IEnumerable<Rating>>(response);
                        if (rest.TblOffer.Any())
                        {
                            rest.Budget = rest.TblOffer.Average(x => x.Price);
                        }

                        if (restaurantRating.Any())
                        {
                            rest.Rating = restaurantRating.Average(x => Convert.ToDecimal(x.Rest_Rating));
                        }
                    }
                }
            }

            _database.SaveChanges();
        }
    }
}
