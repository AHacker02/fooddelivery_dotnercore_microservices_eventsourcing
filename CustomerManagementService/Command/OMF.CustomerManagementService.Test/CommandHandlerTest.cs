using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using OMF.CustomerManagementService.Command.Api.Application;
using OMF.CustomerManagementService.Command.Repository.Abstractions;
using OMF.CustomerManagementService.Command.Repository.DataContext;
using OMF.CustomerManagementService.Command.Service.Command;
using OMF.CustomerManagementService.Command.Service.CommandHandlers;

namespace OMF.CustomerManagementService.Test
{
    [TestFixture]
    public class CommandHandlerTest
    {
        private Mock<IAuthRepository> _mockAuthrepo;
        private IMapper _mapper;

        public CommandHandlerTest()
        {
            var myProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _mockAuthrepo=new Mock<IAuthRepository>();
        }
        
        
        [Test]
        [TestCase("{\n  \"firstName\": \"string\",\n  \"lastName\": \"string\",\n  \"email\": \"string\",\n  \"mobileNumber\": \"string\",\n  \"password\": \"string\",\n  \"address\": \"string\"\n}",400,true)]
        [TestCase("{\n  \"firstName\": \"string\",\n  \"lastName\": \"string\",\n  \"email\": \"string\",\n  \"mobileNumber\": \"string\",\n  \"password\": \"string\",\n  \"address\": \"string\"\n}",200,false)]
        public async Task CreateUserCommandHandlerTest(string commandjson,int expectedStatus,bool userExists)
        {
            _mockAuthrepo.Setup(x => x.UserExists(It.IsAny<string>())).Returns(Task.FromResult(userExists));
            _mockAuthrepo.Setup(x => x.Register(It.IsAny<TblCustomer>(),It.IsAny<string>())).Returns(Task.FromResult(new TblCustomer()));
            var handler=new CreateUserCommandHandler(_mockAuthrepo.Object,_mapper);
            var command = JsonConvert.DeserializeObject<CreateUserCommand>(commandjson);
            var result = await handler.Handle(command,new CancellationToken());
            
            Assert.AreEqual(expectedStatus,result.Code);
        }

        [Test]
        [TestCase("{\n  \"firstName\": \"string\",\n  \"lastName\": \"string\",\n  \"email\": \"string\",\n  \"mobileNumber\": \"string\",\n  \"password\": \"string\",\n  \"address\": \"string\"\n}",200,true)]
        [TestCase("{\n  \"firstName\": \"string\",\n  \"lastName\": \"string\",\n  \"email\": \"string\",\n  \"mobileNumber\": \"string\",\n  \"password\": \"string\",\n  \"address\": \"string\"\n}",400,false)]
        public async Task DeleteUserCommandHandlerTest(string commandjson, int expectedStatus, bool userExists)
        {
            _mockAuthrepo.Setup(x => x.UserExists(It.IsAny<string>())).Returns(Task.FromResult(userExists));
            _mockAuthrepo.Setup(x => x.DeleteUser(It.IsAny<TblCustomer>(),It.IsAny<string>()));
            var handler=new DeleteUserCommandHandler(_mockAuthrepo.Object,_mapper);
            var command = JsonConvert.DeserializeObject<DeleteUserCommand>(commandjson);
            var result = await handler.Handle(command,new CancellationToken());
            
            Assert.AreEqual(expectedStatus,result.Code);
        }
        
        
        [Test]
        [TestCase("{\n  \"firstName\": \"string\",\n  \"lastName\": \"string\",\n  \"email\": \"string\",\n  \"mobileNumber\": \"string\",\n  \"password\": \"string\",\n  \"address\": \"string\"\n}",200,true)]
        public async Task UpdateUserCommandHandlerTest(string commandjson, int expectedStatus, bool userExists)
        {
            _mockAuthrepo.Setup(x => x.UpdateUser(It.IsAny<TblCustomer>(),It.IsAny<string>()));
            var handler=new UpdateUserCommandHandler(_mockAuthrepo.Object,_mapper);
            var command = JsonConvert.DeserializeObject<UpdateUserCommand>(commandjson);
            var result = await handler.Handle(command,new CancellationToken());
            
            Assert.AreEqual(expectedStatus,result.Code);
        }
        
    }
}