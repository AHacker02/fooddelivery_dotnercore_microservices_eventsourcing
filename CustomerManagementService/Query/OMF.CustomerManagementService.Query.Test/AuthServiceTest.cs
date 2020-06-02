using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OMF.Common.Models;
using OMF.CustomerManagementService.Query.Repository.Abstractions;
using OMF.CustomerManagementService.Query.Repository.Models;
using OMF.CustomerManagementService.Query.Service;
using OMF.CustomerManagementService.Query.Service.Abstractions;

namespace OMF.CustomerManagementService.Query.Test
{
    [TestFixture]
    public class AuthServiceTest
    {
        private Mock<IAuthRepository> _mockAuthRepo;
        private IConfiguration _mockConfiguration;

        private IAuthService _authService;

        public AuthServiceTest()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Token", "76a4ab1f-56ce-4e4b-a5bf-dbc61eef0602"},
                
            };

            _mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            _mockAuthRepo = new Mock<IAuthRepository>();
        }

        [Test]
        [TestCase("email", "wrongpassword")]
        public async Task Login_WrongPassword_Null(string email, string password)
        {
            _mockAuthRepo.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult((User) null));
            _authService = new AuthService(_mockAuthRepo.Object, _mockConfiguration);

            var result = await _authService.Login(email, password);

            Assert.IsNull(result);
        }

        [Test]
        [TestCase("email", "correctpassword")]
        public async Task Login_CorrectPassword_UserAuthToken(string email, string password)
        {
            _mockAuthRepo.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new User() {Id = 1, Email = "Test"}));
            _authService = new AuthService(_mockAuthRepo.Object, _mockConfiguration);

            var result = await _authService.Login(email, password);

            Assert.That(result, Is.TypeOf<UserAuthToken>());
        }
    }
}