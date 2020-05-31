using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OMF.RestaurantService.Command.Service.Command;

namespace OMF.RestaurantService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : AppControllerBase
    {
        private readonly IMediator _service;

        public RestaurantController(IConfiguration configuration,IMediator service):base(configuration)
        {
            _service = service;
        }

        public async Task<IActionResult> UpdateItemPrice(PriceUpdateCommand command)
        {
            var response = await _service.Send(command);

            return StatusCode(response.Code, response.Message);
        }
    }
}