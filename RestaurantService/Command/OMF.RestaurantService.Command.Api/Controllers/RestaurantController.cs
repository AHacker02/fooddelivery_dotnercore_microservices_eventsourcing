using BaseService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OMF.RestaurantService.Command.Service.Command;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OMF.RestaurantService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : AppControllerBase
    {
        private readonly IMediator _service;
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(IConfiguration configuration, IMediator service,ILogger<RestaurantController> logger) : base(configuration)
        {
            _service = service;
            _logger = logger;
        }


        /// <summary>
        /// POST api/restaurant/updateprice
        /// To update item price
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Response</returns>
        [HttpPost("updateprice")]
        public async Task<IActionResult> UpdateItemPrice(PriceUpdateCommand command)
        {
            var response = await _service.Send(command);

            _logger.LogInformation("Item {item} price updated to {}",command.MenuId,command.Price);
            return StatusCode(response.Code, response.Message);
        }
    }
}