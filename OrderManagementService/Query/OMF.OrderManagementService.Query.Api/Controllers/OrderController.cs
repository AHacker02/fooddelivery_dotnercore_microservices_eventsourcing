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
        

        /// <summary>
        /// GET api/order/food
        /// </summary>
        /// <returns>All user orders</returns>
        [HttpGet("food")]
        public async Task<IActionResult> GetUserOrders()
        {
            return Ok(await _orderService.GetUserOrders(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        
        /// <summary>
        /// GET api/order/food/{Id}
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Returns single user order</returns>
        [HttpGet("food/{orderId}")]
        public async Task<IActionResult> GetUserOrders(int orderId)
        {
            return Ok(await _orderService.GetUserOrdersById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),orderId));
        }
        
        
        /// <summary>
        /// GET api/order/table 
        /// </summary>
        /// <returns>List of user table bookings</returns>
        [HttpGet("table")]
        public async Task<IActionResult> GetUserBookings()
        {
            return Ok(await _orderService.GetUserBookings(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        
        /// <summary>
        /// GET api/order/{Id}
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns>Single user booking</returns>
        [HttpGet("table/{bookingId}")]
        public async Task<IActionResult> GetUserBookings(int bookingId)
        {
            return Ok(await _orderService.GetUserBookingsById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),bookingId));
        }

        
        /// <summary>
        /// GET api/order/transactions
        /// </summary>
        /// <returns>List of user transations</returns>
        [HttpGet("transactions")]
        public async Task<IActionResult> GetUserTransactions()
        {
            return Ok(await _orderService.GetUserTransactions(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }
        
        
        /// <summary>
        /// GET api/order/cart
        /// </summary>
        /// <returns>List of user items in cart</returns>
        [HttpGet("cart")]
        public async Task<IActionResult> GetUserCart()
        {
            return Ok(await _orderService.GetUserCart(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }
    }
}