using Autofac;
using OMF.Common.Events;
using OMF.RestaurantService.Command.Application.EventHandlers;
using OMF.RestaurantService.Command.Application.Repositories;
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
            builder.RegisterType<Seed>();
        }
    }
}
