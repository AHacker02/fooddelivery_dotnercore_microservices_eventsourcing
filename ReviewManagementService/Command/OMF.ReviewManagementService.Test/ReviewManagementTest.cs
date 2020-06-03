using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using OMF.Common.Models;
using OMF.ReviewManagementService.Command.Controllers;
using OMF.ReviewManagementService.Command.Service.Command;

namespace OMF.ReviewManagementService.Test
{
    [TestFixture]
    public class ReviewManagementTest
    {
        private ReviewController _controller;
        private Mock<IMediator> _mediator;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<ILogger<ReviewController>> _logger;
        private Dictionary<object, object> _httpContextItems = new Dictionary<object, object>();
        private  Mock<HttpContext> mockHttpContext;

        public ReviewManagementTest()
        {
            _mediator = new Mock<IMediator>();
            _mediator.Setup(x => x.Send(It.IsAny<IRequest<Response>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Response(200, "response")));
            _mockConfiguration = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<ReviewController>>();
            
            
            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                    new ClaimsIdentity(
                        new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "0") ,
                            new Claim(ClaimTypes.Name, "Name") 

                        })
                });

            var mockHttpContext = new Mock<HttpContext>(MockBehavior.Strict);
            mockHttpContext.SetupGet(hc => hc.User).Returns(validPrincipal);
            mockHttpContext.SetupGet(c => c.Items).Returns(_httpContextItems);
            
            _controller = new ReviewController(_mediator.Object, _mockConfiguration.Object, _logger.Object);
            _controller.ControllerContext=new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };
        }
        
        [Test]
        [TestCase("{\r\n  \"rating\": \"string\",\r\n  \"comments\": \"string\",\r\n  \"restaurantId\": 0,\r\n  \"customerId\": 0\r\n}",200)]
        public async Task RegisterTest(string json,int expectedStatus)
        {
            var command = JsonConvert.DeserializeObject<ReviewCommand>(json);
            var result = await _controller.AddReview(command) as ObjectResult;
            
            Assert.AreEqual(expectedStatus,result.StatusCode);
        }
    }
}