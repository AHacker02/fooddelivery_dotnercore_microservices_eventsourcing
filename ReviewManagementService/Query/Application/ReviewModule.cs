using Autofac;
using OMF.ReviewManagementService.Query.Application.Repositories;
using OMF.ReviewManagementService.Query.Application.Services;

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
