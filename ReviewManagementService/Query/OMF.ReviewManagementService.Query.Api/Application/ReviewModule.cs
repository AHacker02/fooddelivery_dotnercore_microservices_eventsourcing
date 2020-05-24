using Autofac;
using Microsoft.Extensions.Configuration;
using OMF.ReviewManagementService.Query.Repository;
using OMF.ReviewManagementService.Query.Repository.Abstractions;
using OMF.ReviewManagementService.Query.Repository.DataContext;
using OMF.ReviewManagementService.Query.Service;
using OMF.ReviewManagementService.Query.Service.Abstractions;

namespace OMF.ReviewManagementService.Query.Application
{
    public class ReviewModule : Module
    {
        private readonly IConfiguration _configuration;

        public ReviewModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReviewService>().As<IReviewService>();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            builder.Register(c => new RatingDataContext(_configuration["ConnectionString:SqlServer"]));
        }
    }
}
