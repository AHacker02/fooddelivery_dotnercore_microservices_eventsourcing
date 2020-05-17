using Autofac;
using OMF.ReviewManagementService.Query.Repository;
using OMF.ReviewManagementService.Query.Repository.Abstractions;
using OMF.ReviewManagementService.Query.Service;
using OMF.ReviewManagementService.Query.Service.Abstractions;

namespace OMF.ReviewManagementService.Query.Application
{
    public class ReviewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReviewService>().As<IReviewService>();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
        }
    }
}
