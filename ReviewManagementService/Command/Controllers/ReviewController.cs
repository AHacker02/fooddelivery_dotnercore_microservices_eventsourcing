using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.ReviewManagementService.Command.Application.Command;
using ServiceBus.Abstractions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OMF.ReviewManagementService.Command.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
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
            command.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);

            return Accepted();

        }
    }
}