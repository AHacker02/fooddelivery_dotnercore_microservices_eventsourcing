using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OMF.CustomerManagementService.Query.Api.Controllers;
using OMF.CustomerManagementService.Query.Repository.Models;
using OMF.CustomerManagementService.Query.Service.Abstractions;

namespace OMF.CustomerManagementService.Query.Test
{
    [TestFixture]
    public class AuthControllerTest
    {
        private AuthController _controller;
        private Mock<IAuthService> _mockAuthService;
        private Mock<IConfiguration> _mockConfiguration;

        public AuthControllerTest()
        {
            _mockAuthService=new Mock<IAuthService>();
            _mockConfiguration=new Mock<IConfiguration>();
        }

        [Test]
        [TestCase(null,"password")]
        [TestCase("email",null)]
        public async Task Login_InValidEmailAndPassword_Badrequest(string email,string password)
        {
            _controller= new AuthController(_mockAuthService.Object,_mockConfiguration.Object);

            var result = await _controller.Login(email, password);
            
            Assert.That(result,Is.TypeOf<BadRequestObjectResult>());
        }
        
        
        [Test]
        [TestCase("email","password")]
        public async Task Login_WrongEmailAndPassword_Unauthorized(string email,string password)
        {
            _mockAuthService.Setup(x => x.Login(email, password)).Returns(Task.FromResult((UserAuthToken)null));
            _controller= new AuthController(_mockAuthService.Object,_mockConfiguration.Object);

            var result = await _controller.Login(email, password);
            
            Assert.That(result,Is.TypeOf<UnauthorizedResult>());
        }
        
        [Test]
        [TestCase("email","password")]
        public async Task Login_CorrectEmailAndPassword_Ok(string email,string password)
        {
            _mockAuthService.Setup(x => x.Login(email, password)).Returns(Task.FromResult(new UserAuthToken()));
            _controller= new AuthController(_mockAuthService.Object,_mockConfiguration.Object);

            var result = await _controller.Login(email, password);
            
            Assert.That(result,Is.TypeOf<OkObjectResult>());
        }
        
    }
}