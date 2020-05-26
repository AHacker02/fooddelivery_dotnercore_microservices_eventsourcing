using Autofac;
using Microsoft.Extensions.Configuration;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Repository;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.CommandHandlers;
using OMF.OrderManagementService.Command.Service.Commands;
using OMF.OrderManagementService.Command.Service.EventHandlers;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Application
{
    public class OrderModule : Module
    {
        private readonly IConfiguration _configuration;

        public OrderModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderCommandHandler>().As<ICommandHandler<OrderCommand>>();
            builder.RegisterType<PaymentCommandHandler>().As<ICommandHandler<PaymentCommand>>();
            builder.RegisterType<CancelOrderCommandHandler>().As<ICommandHandler<CancelOrderCommand>>();
            builder.RegisterType<DeliveryEventHandler>().As<IEventHandler<OrderReadyEvent>>();
            builder.RegisterType<PaymentEventHandler>().As<IEventHandler<PaymentInitiatedEvent>>();
            builder.Register(c => new OrderManagementContext(_configuration["ConnectionString:SqlServer"]));
        }
    }
}
