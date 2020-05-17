using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.RestaurantService.Repository.Abstractions
{
    public interface IRestaurantRepository
    {
        Task AddRestaurantsAsync(IEnumerable<Restaurant> restaurants);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task UpdateStock(List<Item> orderItems);
        Task UpdateRating(Guid restaurantId, string rating);

    }
}
