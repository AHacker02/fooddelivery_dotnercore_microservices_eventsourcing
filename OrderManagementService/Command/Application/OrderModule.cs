using Autofac;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Application.CommandHandlers;
using OMF.OrderManagementService.Command.Application.Commands;
using OMF.OrderManagementService.Command.Application.EventHandlers;
using OMF.OrderManagementService.Command.Application.Events;
using OMF.OrderManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Application
{
    public class OrderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderCommandHandler>().As<ICommandHandler<OrderCommand>>();
            builder.RegisterType<CancelOrderCommandHandler>().As<ICommandHandler<CancelOrderCommand>>();
            builder.RegisterType<DeliveryEventHandler>().As<IEventHandler<OrderReadyEvent>>();
            builder.RegisterType<PaymentEventHandler>().As<IEventHandler<PaymentInitiatedEvent>>();
        }
    }
}
