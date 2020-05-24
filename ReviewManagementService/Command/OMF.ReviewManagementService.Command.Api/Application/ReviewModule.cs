using Autofac;
using OMF.ReviewManagementService.Command.Repository;
using OMF.ReviewManagementService.Command.Repository.Abstractions;
using OMF.ReviewManagementService.Command.Service.Command;
using OMF.ReviewManagementService.Command.Service.CommandHandlers;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Command.Application
{
    public class ReviewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            builder.RegisterType<ReviewUpdateCommandHandler>().As<ICommandHandler<ReviewCommand>>();
        }
    }
}