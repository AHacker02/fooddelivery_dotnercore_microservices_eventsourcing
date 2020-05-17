using Autofac;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Repository;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Service.CommandHandlers;
using OMF.OrderManagementService.Command.Service.Commands;
using OMF.OrderManagementService.Command.Service.EventHandlers;
using OMF.OrderManagementService.Command.Service.Events;
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
