using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using OMF.Common.Models;
using OMF.CustomerManagementService.Query.Api.Application;
using OMF.CustomerManagementService.Query.Repository;
using OMF.CustomerManagementService.Query.Repository.Abstractions;
using OMF.CustomerManagementService.Query.Repository.DataContext;

namespace OMF.CustomerManagementService.Query.Test
{
    [TestFixture]
    public class AuthRepositoryTest
    {
        private IAuthRepository _authRepository;
        private IMapper _mapper;
        private CustomerManagementContext context;

        public AuthRepositoryTest()
        {
            var myProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            var options = new DbContextOptionsBuilder<CustomerManagementContext>()
                .UseInMemoryDatabase(databaseName: "MovieListDatabase")
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
        public async Task Login_CorrectPassword_User()
        {
            _authRepository = new AuthRepository(context, _mapper);

            var result = await _authRepository.Login("email@test.com", "TestPassword");

            Assert.IsNotNull(result);
        }
        
        [Test]
        public async Task Null()
        {
            _authRepository = new AuthRepository(context, _mapper);

            var result = await _authRepository.Login("email@test.com", "WrongPassword");

            Assert.IsNull(result);
        }
    }
}

