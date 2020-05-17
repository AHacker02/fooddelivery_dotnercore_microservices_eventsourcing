using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.RestaurantService.Query.Service.Abstractions;

namespace OMF.RestaurantService.Query.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public RestaurantController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("")]
        public async Task<IActionResult> SearchRestaurants(string Id = null, string name = null, string location = null,
            string budget = null, string rating = null, string food = null)
        {
            var result = await _searchService.SearchRestaurant(Id, name, location, budget, rating, food);
            return Ok(result);
        }
    }
}