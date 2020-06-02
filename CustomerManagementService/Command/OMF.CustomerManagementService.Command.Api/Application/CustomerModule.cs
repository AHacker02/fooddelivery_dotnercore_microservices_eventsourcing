using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using OMF.Common.Models;
using OMF.CustomerManagementService.Command.Repository;
using OMF.CustomerManagementService.Command.Repository.Abstractions;
using OMF.CustomerManagementService.Command.Repository.DataContext;
using OMF.CustomerManagementService.Command.Service.Command;
using OMF.CustomerManagementService.Command.Service.CommandHandlers;
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Api.Application
{
    public class CustomerModule : Module
    {
        private readonly IConfiguration _configuration;

        public CustomerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //Service Registrations
            builder.RegisterType<AuthRepository>().As<IAuthRepository>();

            //Handler Registrations
            builder.RegisterType<CreateUserCommandHandler>().As<IRequestHandler<CreateUserCommand,Response>>();
            builder.RegisterType<DeleteUserCommandHandler>().As<IRequestHandler<DeleteUserCommand,Response>>();
            builder.RegisterType<UpdateUserCommandHandler>().As<IRequestHandler<UpdateUserCommand,Response>>();
        }
    }
}