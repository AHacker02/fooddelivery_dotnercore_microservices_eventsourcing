using System.Threading.Tasks;
using BaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OMF.CustomerManagementService.Query.Service.Abstractions;

namespace OMF.CustomerManagementService.Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService,IConfiguration configuration):base(configuration)
        {
            _authService = authService;
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
                return Ok(response);

            return Unauthorized();
        }
    }
}