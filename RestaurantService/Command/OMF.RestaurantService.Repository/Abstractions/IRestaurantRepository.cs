using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.DataContext;

namespace OMF.RestaurantService.Repository.Abstractions
{
    public interface IRestaurantRepository
    {
        Task AddRestaurantsAsync(IEnumerable<Restaurant> restaurants);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task UpdateStock(List<TblMenu> orderItems);
        Task UpdateRating(int restaurantId, string rating);

    }
}
