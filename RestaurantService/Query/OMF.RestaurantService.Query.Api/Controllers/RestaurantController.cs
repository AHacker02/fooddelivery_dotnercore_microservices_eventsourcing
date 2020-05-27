﻿using System.Threading.Tasks;
using BaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.RestaurantService.Query.Service.Abstractions;

namespace OMF.RestaurantService.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : AppControllerBase
    {
        private readonly ISearchService _searchService;

        public RestaurantController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRestaurants(string id=null,string name = null, string coordinateX = null, string coordinateY=null,
            string budget = null, string rating = null, string food = null,string distance="5", string cuisine=null)
        {
            var result = await _searchService.SearchRestaurant(id,name, coordinateX,coordinateY, budget, rating, food,distance,cuisine);
            return Ok(result);
        }
    }
}