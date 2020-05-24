using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.ReviewManagementService.Command.Service.Command;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : AppControllerBase
    {
        private readonly IEventBus _bus;

        public ReviewController(IEventBus bus)
        {
            _bus = bus;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddReview(ReviewCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);

            return Accepted();
        }
    }
}