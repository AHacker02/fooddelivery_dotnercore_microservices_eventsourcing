using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OMF.OrderManagementService.Query.Service.Abstractions;

namespace OMF.OrderManagementService.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserOrders()
        {
            return Ok(await _orderService.GetUserOrders(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetUserOrders(string orderId)
        {
            return Ok((await _orderService.GetUserOrders(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))).FirstOrDefault(x => x.Id == Guid.Parse(orderId)));
        }
    }
}