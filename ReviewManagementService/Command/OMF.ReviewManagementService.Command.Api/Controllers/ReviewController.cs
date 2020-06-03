using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OMF.ReviewManagementService.Command.Service.Command;
using Serilog;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : AppControllerBase
    {
        private readonly IMediator _service;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(IMediator service,IConfiguration configuration,ILogger<ReviewController> logger):base(configuration)
        {
            _service = service;
            _logger = logger;
        }


        /// <summary>
        /// POST api/review
        /// To add review to restaurant
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Response</returns>
        [HttpPost("")]
        public async Task<IActionResult> AddReview(ReviewCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            _logger.LogInformation("User: {Email} Restaurant: {restaurant} Rating:{rating}",User.FindFirst(ClaimTypes.Name).Value,command.RestaurantId,command.Rating);
            return StatusCode(result.Code,result.Message);
        }
    }
}