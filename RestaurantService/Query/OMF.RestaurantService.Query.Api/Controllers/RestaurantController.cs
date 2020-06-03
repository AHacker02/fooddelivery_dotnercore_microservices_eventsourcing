using BaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OMF.RestaurantService.Query.Service.Abstractions;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : AppControllerBase
    {
        private readonly ISearchService _searchService;

        public RestaurantController(ISearchService searchService, IConfiguration configuration) : base(configuration)
        {
            _searchService = searchService;
        }


        /// <summary>
        /// GET api/restaurant/search
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
        /// <returns>List of restaurants</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchRestaurants(string id = null, string name = null, string coordinateX = null, string coordinateY = null,
            string budget = null, string rating = null, string food = null, string distance = "5", string cuisine = null)
        {
            var result = await _searchService.SearchRestaurant(id, name, coordinateX, coordinateY, budget, rating, food, distance, cuisine);
            return Ok(result);
        }
    }
}