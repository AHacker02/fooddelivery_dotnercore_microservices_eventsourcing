using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.Abstractions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using OMF.OrderManagementService.Command.Service.Commands;

namespace OMF.OrderManagementService.Command.Controllers
{
    [Route("api/[controller]")]    
    public class OrderController : AppControllerBase
    {
        private readonly IEventBus _bus;

        public OrderController(IEventBus bus)
        {
            _bus = bus;
        }

        [HttpPost("order")]
        public async Task<IActionResult> OrderFood([FromBody]OrderCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);
            return Accepted();
        }
        
        [HttpPost("payment")]
        [AllowAnonymous]
        public async Task<IActionResult> Payment([FromBody]PaymentCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);
            return Accepted();
        }

        [HttpPost("booking")]
        public async Task<IActionResult> Payment([FromBody]TableBookingCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);
            return Accepted();
        }

        [HttpPut("orderupdate")]
        public async Task<IActionResult> UpdateOrder([FromBody]OrderUpdateCommand command)
        {
            //command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);
            return Accepted();
        }

        [HttpDelete("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody]CancelOrderCommand command)
        {
            command.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bus.PublishCommand(command);
            return Accepted();
        }
    }
}