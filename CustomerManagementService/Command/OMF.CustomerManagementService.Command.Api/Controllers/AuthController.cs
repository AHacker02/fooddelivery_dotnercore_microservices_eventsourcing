using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.CustomerManagementService.Command.Service.Command;
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private readonly IEventBus _bus;

        public AuthController(IEventBus bus)
        {
            _bus = bus;
        }


        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bus.PublishCommand(command);

            return Accepted();
        }

        
        /// <summary>
        /// Deactivate account
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete("deactivate")]
        public async Task<IActionResult> Deactivate(DeleteUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bus.PublishCommand(command);

            return Accepted();
        }

        /// <summary>
        /// Update user details except email
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("updatedetails")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            command.Email = User.FindFirst(ClaimTypes.Name).Value;
            await _bus.PublishCommand(command);

            return Accepted();
        }
    }
}