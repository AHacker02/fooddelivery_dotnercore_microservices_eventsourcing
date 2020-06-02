using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OMF.Common.Models;
using OMF.ReviewManagementService.Query.Application;
using OMF.ReviewManagementService.Query.Repository;
using OMF.ReviewManagementService.Query.Repository.Abstractions;
using OMF.ReviewManagementService.Query.Repository.DataContext;
using OMF.ReviewManagementService.Query.Service;
using OMF.ReviewManagementService.Query.Service.Abstractions;

namespace OMF.ReviewManagementService.Test
{
    [TestFixture]
    public class ReviewRepositoryTest
    {
        private IReviewRepository _repository;
        private RatingDataContext context;
        private IMapper _mapper;    

        public ReviewRepositoryTest()
        {
            
            var myProfile = new ReviewProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
           var options = new DbContextOptionsBuilder<RatingDataContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            context = new RatingDataContext(options);

            using (var hmac = new HMACSHA512())
            {
                context.TblRating.Add(new TblRating()
                {
                    TblRestaurantId = 1
                });
            }

            context.SaveChanges();
        }



        [Test]
        public async Task GetRestaurantRatingTest()
        {
            
            _repository=new ReviewRepository(context,_mapper);

            var result = await _repository.GetRestaurantReviews(1);
            
            Assert.AreEqual(1,result.Count());
        }
    }
}