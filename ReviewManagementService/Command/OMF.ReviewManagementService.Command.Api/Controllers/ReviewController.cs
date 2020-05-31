using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OMF.ReviewManagementService.Command.Service.Command;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : AppControllerBase
    {
        private readonly IMediator _service;

        public ReviewController(IMediator service,IConfiguration configuration):base(configuration)
        {
            _service = service;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddReview(ReviewCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code,result.Message);
        }
    }
}