using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BaseService;
using Microsoft.Extensions.Configuration;
using OMF.ReviewManagementService.Query.Service.Abstractions;

namespace OMF.ReviewManagementService.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : AppControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService,IConfiguration configuration):base(configuration)
        {
            _reviewService = reviewService;
        }
        

        /// <summary>
        /// GET api/Review
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns>List of reviews</returns>
        [HttpGet("")]
        public async Task<IActionResult> RestaurantReviews(int restaurantId)
        {
            var result = await _reviewService.GetRestaurantReviews(restaurantId);
            return Ok(result);
        }
    }
}