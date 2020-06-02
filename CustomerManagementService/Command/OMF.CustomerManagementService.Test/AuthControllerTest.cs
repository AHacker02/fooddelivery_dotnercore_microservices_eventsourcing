using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using OMF.Common.Models;
using OMF.CustomerManagementService.Command.Api.Controllers;
using OMF.CustomerManagementService.Command.Service.Command;
using Serilog.Core;
using ILogger = Serilog.ILogger;

namespace OMF.CustomerManagementService.Test
{
    [TestFixture]
    public class AuthControllerTest
    {
        private AuthController _controller;
        private Mock<IMediator> _mediator;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<ILogger<AuthController>> _logger;

        public AuthControllerTest()
        {
            _mediator=new Mock<IMediator>();
            _mediator.Setup(x => x.Send(It.IsAny<IRequest<Response>>(),It.IsAny<CancellationToken>())).Returns(Task.FromResult(new Response(200,"response")));
            _mockConfiguration=new Mock<IConfiguration>();
            _logger = new Mock<ILogger<AuthController>>();
            _controller= new AuthController(_mediator.Object,_mockConfiguration.Object,_logger.Object);
        }
        
        [Test]
        [TestCase("{\r\n  \"firstName\": \"string\",\r\n  \"lastName\": \"string\",\r\n  \"email\": \"string\",\r\n  \"mobileNumber\": \"string\",\r\n  \"password\": \"string\",\r\n  \"address\": \"string\"\r\n}",200)]
        public async Task RegisterTest(string json,int expectedStatus)
        {
            var command = JsonConvert.DeserializeObject<CreateUserCommand>(json);
            var result = await _controller.Register(command) as ObjectResult;
            
            Assert.AreEqual(expectedStatus,result.StatusCode);
        }
        
        [Test]
        [TestCase("{\n  \"email\": \"string\",\n  \"password\": \"string\"\n}",200)]
        public async Task DeactivateTest(string json,int expectedStatus)
        {
            var command = JsonConvert.DeserializeObject<DeleteUserCommand>(json);
            var result = await _controller.Deactivate(command) as ObjectResult;
            
            Assert.AreEqual(expectedStatus,result.StatusCode);
        }
        
        [Test]
        [TestCase("{\r\n  \"firstName\": \"string\",\r\n  \"lastName\": \"string\",\r\n  \"email\": \"string\",\r\n  \"mobileNumber\": \"string\",\r\n  \"password\": \"string\",\r\n  \"address\": \"string\"\r\n}",200)]
        public async Task UpdateTest(string json,int expectedStatus)
        {
            var command = JsonConvert.DeserializeObject<UpdateUserCommand>(json);
            var result = await _controller.Update(command) as ObjectResult;
            
            Assert.AreEqual(expectedStatus,result.StatusCode);
        }
    }

    
}