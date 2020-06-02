using System.Threading.Tasks;
using BaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OMF.CustomerManagementService.Query.Service.Abstractions;
using Serilog;

namespace OMF.CustomerManagementService.Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService,IConfiguration configuration,ILogger<AuthController> logger):base(configuration)
        {
            _authService = authService;
            _logger = logger;
        }


        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Auth token 
        /// User details
        /// </returns>
        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return BadRequest("The Username or Password is invalid");
            

            var response = await _authService.Login(email, password);
            if (response != null)
            {
                _logger.LogInformation("User: {email} logged in.",email);
                return Ok(response);
            }

            return Unauthorized();
        }
    }
}