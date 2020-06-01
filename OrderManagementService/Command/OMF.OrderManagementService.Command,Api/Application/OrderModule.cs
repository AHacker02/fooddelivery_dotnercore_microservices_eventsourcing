﻿using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using OMF.Common.Abstractions;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
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
            //register services
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<HttpWrapper>().As<IHttpWrapper>();

            //command handler 
            builder.RegisterType<OrderCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<PaymentCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<CancelOrderCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<TableBookingCommandHandler>().AsImplementedInterfaces();

            //event handler
            builder.RegisterType<ItemOutOfStockEventHandler>().As<IEventHandler<ItemOutOfStockEvent>>();
            builder.RegisterType<PaymentEventHandler>().As<IEventHandler<PaymentInitiatedEvent>>();
            builder.RegisterType<ItemPriceUpdateEventHandler>().As<IEventHandler<ItemPriceUpdateEvent>>();
        }
    }
}
