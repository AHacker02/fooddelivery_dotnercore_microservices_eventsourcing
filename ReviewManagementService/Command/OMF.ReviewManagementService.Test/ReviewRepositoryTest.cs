using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OMF.ReviewManagementService.Command.Application;
using OMF.ReviewManagementService.Command.Repository;
using OMF.ReviewManagementService.Command.Repository.Abstractions;
using OMF.ReviewManagementService.Command.Repository.DataContext;

namespace OMF.ReviewManagementService.Test
{
    [TestFixture]
    public class ReviewRepositoryTest
    {
        private Mapper _mapper;
        private RatingDataContext context;
        private IReviewRepository _repository;


        public ReviewRepositoryTest()
        {
            var myProfile = new ReviewProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            var options = new DbContextOptionsBuilder<RatingDataContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            

            context = new RatingDataContext(options);

            context.TblRating.Add(new TblRating()
            {
                TblRestaurantId = 1,
                Rating = "2",
                TblCustomerId = 1
            });

            context.SaveChanges();
        }


        [Test]
        [TestCase(1, "2")]
        public async Task GetRestaurantReviewsTest(int restaurantId, string expectedResult)
        {
            _repository = new ReviewRepository(context);

            var result = await _repository.GetRestaurantReviews(restaurantId);

            Assert.AreEqual(expectedResult, result.FirstOrDefault().Rating);
        }


        [Test]
        public async Task UpsertReviewTest()
        {
            _repository = new ReviewRepository(context);

            var result= _repository.UpsertReview(new TblRating()
            {
                TblCustomerId = 1,
                TblRestaurantId = 1,
                Rating = "3"
            });

            Assert.AreEqual(true, result.IsCompleted);
        }

    }
}
