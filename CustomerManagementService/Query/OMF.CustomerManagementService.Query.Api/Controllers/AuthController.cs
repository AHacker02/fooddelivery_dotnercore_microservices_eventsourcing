using Microsoft.AspNetCore.Mvc;
using OMF.CustomerManagementService.Query.Application.Services;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet("login")]
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