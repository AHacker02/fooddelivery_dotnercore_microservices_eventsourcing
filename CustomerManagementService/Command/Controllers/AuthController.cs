using Microsoft.AspNetCore.Mvc;
using OMF.CustomerManagementService.Command.Application.Command;
using OMF.CustomerManagementService.Command.Application.Event;
using ServiceBus.Abstractions;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEventBus _bus;

        public AuthController(IEventBus bus)
        {
            _bus = bus;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bus.PublishCommand(command);

            return Accepted();
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Register(DeleteUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bus.PublishCommand(command);

            return Accepted();
        }
    }
}