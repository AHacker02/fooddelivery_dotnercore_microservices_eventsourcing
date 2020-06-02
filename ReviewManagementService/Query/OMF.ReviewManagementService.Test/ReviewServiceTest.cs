using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OMF.Common.Models;
using OMF.ReviewManagementService.Query.Controllers;
using OMF.ReviewManagementService.Query.Repository.Abstractions;
using OMF.ReviewManagementService.Query.Service;
using OMF.ReviewManagementService.Query.Service.Abstractions;

namespace OMF.ReviewManagementService.Test
{
    [TestFixture]
    public class ReviewServiceTest
    {
        private Mock<IReviewRepository> _repository;
        private IReviewService _service;
        private Mock<IConfiguration> _mockConfiguration;

        public ReviewServiceTest()
        {
            _mockConfiguration=new Mock<IConfiguration>();
            _repository=new Mock<IReviewRepository>();
        }



        [Test]
        public async Task GetRestaurantRatingTest()
        {
            _repository.Setup(x => x.GetRestaurantReviews(It.IsAny<int>())).ReturnsAsync(new List<Rating>());
            
            _service=new ReviewService(_repository.Object);

            var result = await _service.GetRestaurantReviews(1);
            
            Assert.NotNull(result);
        }
    }
}