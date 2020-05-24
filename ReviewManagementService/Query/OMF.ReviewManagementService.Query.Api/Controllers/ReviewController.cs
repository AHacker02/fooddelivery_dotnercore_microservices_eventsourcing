using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BaseService;
using OMF.ReviewManagementService.Query.Service.Abstractions;

namespace OMF.ReviewManagementService.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : AppControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpGet("")]
        public async Task<IActionResult> RestaurantReviews(int restaurantId)
        {
            var result = await _reviewService.GetRestaurantReviews(restaurantId);
            return Ok(result);
        }
    }
}