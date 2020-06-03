using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using OMF.ReviewManagementService.Command.Application;
using OMF.ReviewManagementService.Command.Repository.Abstractions;
using OMF.ReviewManagementService.Command.Repository.DataContext;
using OMF.ReviewManagementService.Command.Service.Command;
using OMF.ReviewManagementService.Command.Service.CommandHandlers;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Test
{
    [TestFixture]
    public class CommandHandlerTest
    {
        private Mock<IReviewRepository> _repository;
        private Mock<IEventBus> _bus;
        private IMapper _mapper;

        public CommandHandlerTest()
        {
            var myProfile = new ReviewProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _repository = new Mock<IReviewRepository>();
            _bus=new Mock<IEventBus>();
        }


        [Test]
        [TestCase("{\r\n  \"rating\": \"string\",\r\n  \"comments\": \"string\",\r\n  \"restaurantId\": 0,\r\n  \"customerId\": 0\r\n}", 200)]
        public async Task CreateUserCommandHandlerTest(string commandjson, int expectedStatus)
        {
            _repository.Setup(x => x.UpsertReview(It.IsAny<TblRating>()));
            _repository.Setup(x => x.GetRestaurantReviews(It.IsAny<int>())).ReturnsAsync(new List<TblRating>(){new TblRating(){Rating = "1"}});
            var handler = new ReviewUpdateCommandHandler(_repository.Object,_bus.Object, _mapper);
            var command = JsonConvert.DeserializeObject<ReviewCommand>(commandjson);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.AreEqual(expectedStatus, result.Code);
        }
    }
}
