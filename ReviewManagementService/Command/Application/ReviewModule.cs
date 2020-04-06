using Autofac;
using OMF.ReviewManagementService.Command.Application.Command;
using OMF.ReviewManagementService.Command.Application.CommandHandlers;
using OMF.ReviewManagementService.Command.Application.Event;
using OMF.ReviewManagementService.Command.Application.EventHandlers;
using OMF.ReviewManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Command.Application
{
    public class ReviewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            builder.RegisterType<UpdateRatingEventHandler>().As<IEventHandler<ReviewCreated>>();
            builder.RegisterType<ReviewUpdateCommandHandler>().As<ICommandHandler<ReviewCommand>>();
        }
    }
}
