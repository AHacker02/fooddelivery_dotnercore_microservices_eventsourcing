using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OMF.CustomerManagementService.Command.Service.Command;
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private readonly IMediator _service;
        private readonly ILogger<AuthController> _logger;


        public AuthController(IMediator service,IConfiguration configuration,ILogger<AuthController> logger ):base(configuration)
        {
            _service = service;
            _logger = logger;
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
            var result = await _service.Send(command);

            _logger.LogInformation("User: {Email} Status:{Message}", command.Email, result.Message);
            return StatusCode(result.Code,result.Message);
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

            var result = await _service.Send(command);

            _logger.LogInformation("User: {Email} Status:{Message}", command.Email, result.Message);
            return StatusCode(result.Code, result.Message);
        }

        /// <summary>
        /// Update user details except email
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            command.Email = User.FindFirst(ClaimTypes.Name).Value;

            var result = await _service.Send(command);
            return StatusCode(result.Code, result.Message);
        }
    }
}