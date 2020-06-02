using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OMF.Common.Models;
using OMF.ReviewManagementService.Query.Controllers;
using OMF.ReviewManagementService.Query.Service.Abstractions;

namespace OMF.ReviewManagementService.Test
{
    [TestFixture]
    public class ReviewControllerTest
    {
        private ReviewController _controller;
        private Mock<IReviewService> _service;
        private Mock<IConfiguration> _mockConfiguration;

        public ReviewControllerTest()
        {
            _service=new Mock<IReviewService>();
            _mockConfiguration=new Mock<IConfiguration>();
        }



        [Test]
        public async Task RestaurantRatingTest()
        {
            _service.Setup(x => x.GetRestaurantReviews(It.IsAny<int>())).ReturnsAsync(new List<Rating>());
            
            _controller=new ReviewController(_service.Object,_mockConfiguration.Object);

            var result = await _controller.RestaurantReviews(1) as ObjectResult;
            
            Assert.NotNull(result.Value);
        }
    }
}