using Microsoft.AspNetCore.Mvc;
using OMF.CustomerManagementService.Command.Service.Command;
using ServiceBus.Abstractions;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Command.Api.Controllers
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