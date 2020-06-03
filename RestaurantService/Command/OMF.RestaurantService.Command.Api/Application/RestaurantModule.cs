using Autofac;
using MediatR;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.RestaurantService.Command.Service;
using OMF.RestaurantService.Command.Service.Command;
using OMF.RestaurantService.Command.Service.CommandHandlers;
using OMF.RestaurantService.Command.Service.EventHandlers;
using OMF.RestaurantService.Repository;
using OMF.RestaurantService.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.RestaurantService.Command.Application
{
    public class RestaurantModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>();
            builder.RegisterType<OrderConfirmedEventHandler>().As<IEventHandler<OrderConfirmedEvent>>();
            builder.RegisterType<UpdateRestaurantEventHandler>().As<IEventHandler<UpdateRestaurantEvent>>();
            builder.RegisterType<PriceUpdateCommandHandler>().As<IRequestHandler<PriceUpdateCommand, Response>>();
            builder.RegisterType<Seed>();
        }
    }
}
