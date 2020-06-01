using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using OMF.OrderManagementService.Query.Service.Abstractions;

namespace OMF.OrderManagementService.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : AppControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService,IConfiguration configuration):base(configuration)
        {
            _orderService = orderService;
        }

        [HttpGet("foodorder")]
        public async Task<IActionResult> GetUserOrders()
        {
            return Ok(await _orderService.GetUserOrders(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        [HttpGet("foodorder/{orderId}")]
        public async Task<IActionResult> GetUserOrders(int orderId)
        {
            return Ok(await _orderService.GetUserOrdersById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),orderId));
        }
        
        [HttpGet("tablebooking")]
        public async Task<IActionResult> GetUserBookings()
        {
            return Ok(await _orderService.GetUserBookings(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        [HttpGet("tablebooking/{bookingId}")]
        public async Task<IActionResult> GetUserBookings(int bookingId)
        {
            return Ok(await _orderService.GetUserBookingsById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),bookingId));
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetUserTransactions()
        {
            return Ok(await _orderService.GetUserTransactions(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }
        
        [HttpGet("cart")]
        public async Task<IActionResult> GetUserCart()
        {
            return Ok(await _orderService.GetUserCart(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }
    }
}