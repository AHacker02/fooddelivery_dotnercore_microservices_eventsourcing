using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.Abstractions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using OMF.OrderManagementService.Command.Service.Commands;

namespace OMF.OrderManagementService.Command.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IEventBus _bus;

        public OrderController(IEventBus bus)
        {
            _bus = bus;
        }

        [HttpPost("")]
        public async Task<IActionResult> OrderFood([FromBody]OrderCommand command)
        {
            command.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);
            return Accepted();
        }

        [HttpDelete("")]
        public async Task<IActionResult> CancelOrder([FromBody]CancelOrderCommand command)
        {
            command.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);
            return Accepted();
        }
    }
}