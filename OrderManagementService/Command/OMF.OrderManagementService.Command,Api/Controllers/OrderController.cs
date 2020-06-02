using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OMF.OrderManagementService.Command.Service.Commands;

namespace OMF.OrderManagementService.Command.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : AppControllerBase
    {
        private readonly IMediator _service;

        
        /// <summary>
        /// Constructor for OrderController
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="service"></param>
        public OrderController(IConfiguration configuration, IMediator service) : base(configuration)
        {
            _service = service;
        }

        
        /// <summary>
        /// POST api/order/food
        /// To add to cart/order food
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Order Id</returns>
        [HttpPost("food")]
        public async Task<IActionResult> OrderFood([FromBody] OrderCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }
        
        
        /// <summary>
        /// POST api/order/payment
        /// To pay for order
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Transaction Id</returns>
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] PaymentCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }

        
        /// <summary>
        /// POST api/order/booking
        /// To make table booking
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Booking Id</returns>
        [HttpPost("booking")]
        public async Task<IActionResult> Payment([FromBody] TableBookingCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }

        
        /// <summary>
        /// PUT api/order/food
        /// To update cart/order address
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status</returns>
        [HttpPut("food")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }

        
        /// <summary>
        /// PUT api/order/booking
        /// To update table booking
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status</returns>
        [HttpPut("booking")]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingUpdateCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }
        
        
        /// <summary>
        /// DELETE api/order/cancel
        /// To cancel order
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Refunded ammount</returns>
        [HttpDelete("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderCommand command)
        {
            command.CustomerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.Send(command);

            return StatusCode(result.Code, result.Message);
        }
    }
}