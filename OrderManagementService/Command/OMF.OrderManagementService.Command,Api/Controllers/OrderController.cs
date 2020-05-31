using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.Abstractions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using MediatR;
using Microsoft.Extensions.Configuration;
using OMF.OrderManagementService.Command.Service.Commands;

namespace OMF.OrderManagementService.Command.Controllers
{
    [Route("api/[controller]")]    
    public class OrderController : AppControllerBase
    {
        private readonly IMediator _service;

        public OrderController(IConfiguration configuration,IMediator service):base(configuration)
        {
            _service = service;
        }

        [HttpPost("order")]
        public async Task<IActionResult> OrderFood([FromBody]OrderCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result= await _service.Send(command);

            return StatusCode(result.Code,result.Message);
        }
        
        [HttpPost("payment")]
        [AllowAnonymous]
        public async Task<IActionResult> Payment([FromBody]PaymentCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }

        [HttpPost("booking")]
        public async Task<IActionResult> Payment([FromBody]TableBookingCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }

        [HttpPut("order")]
        public async Task<IActionResult> UpdateOrder([FromBody]OrderUpdateCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }

        [HttpPut("booking")]
        public async Task<IActionResult> UpdateBooking([FromBody]BookingUpdateCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }

        [HttpDelete("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody]CancelOrderCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }
    }
}