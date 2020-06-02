using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using OMF.CustomerManagementService.Command.Api.Application;
using OMF.CustomerManagementService.Command.Repository;
using OMF.CustomerManagementService.Command.Repository.Abstractions;
using OMF.CustomerManagementService.Command.Repository.DataContext;

namespace OMF.CustomerManagementService.Test
{
    [TestFixture]
    public class AuthRepositoryTest
    {
        private Mapper _mapper;
        private CustomerManagementContext context;
        private IAuthRepository _authRepository;


        public AuthRepositoryTest()
        {
            var myProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            var options = new DbContextOptionsBuilder<CustomerManagementContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            context = new CustomerManagementContext(options);

            using (var hmac = new HMACSHA512())
            {
                context.TblCustomer.Add(new TblCustomer()
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "email@test.com",
                    Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("TestPassword")),
                    PasswordKey = hmac.Key,
                    Active = true
                });
            }

            context.SaveChanges();
        }

        [Test]
        [TestCase("email@test.com", true)]
        [TestCase("email@test2.com", false)]
        public async Task UserExistsTest(string email, bool expectedResult)
        {
            _authRepository = new AuthRepository(context);

            var result = await _authRepository.UserExists(email);

            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        [TestCase("{\"Email\":\"email@test.com\"}", "TestPassword")]
        [TestCase("{\"Email\":\"email@test1.com\"}", "TestPassword")]
        public async Task RegisterTest(string emailJson, string password)
        {
            var user = JsonConvert.DeserializeObject<TblCustomer>(emailJson);
            _authRepository = new AuthRepository(context);

            var result = await _authRepository.Register(user, password);

            Assert.IsNotNull(result.Password);
        }


        [Test]
        [TestCase("{\"Email\":\"email@test.com\"}", "TestPassword")]
        public async Task DeleteUserTest(string emailJson, string password)
        {
            var user = JsonConvert.DeserializeObject<TblCustomer>(emailJson);
            _authRepository = new AuthRepository(context);

            await _authRepository.DeleteUser(user, password);

            Assert.IsFalse(await _authRepository.UserExists(user.Email));
        }
        
        
        [Test]
        [TestCase("{\"Email\":\"email@test.com\",\"firstName\":\"Test\"}", "TestPassword")]
        public async Task UpdateUserTest(string emailJson, string password)
        {
            var user = JsonConvert.DeserializeObject<TblCustomer>(emailJson);
            _authRepository = new AuthRepository(context);

            var result= _authRepository.UpdateUser(user, password);

            Assert.IsTrue(result.IsCompleted);
        }
    }
}