﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.ReviewManagementService.Query.Application.Services;
using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagementService.Query.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpGet("")]
        public async Task<IActionResult> RestaurantReviews(Guid restaurantId)
        {
            var result = await _reviewService.GetRestaurantReviews(restaurantId);
            return Ok(result);
        }
    }
}