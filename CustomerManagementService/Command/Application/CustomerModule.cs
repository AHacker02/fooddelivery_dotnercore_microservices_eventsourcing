using Autofac;
using Microsoft.Extensions.Configuration;
using OMF.CustomerManagementService.Command.Application.Command;
using OMF.CustomerManagementService.Command.Application.CommandHandlers;
using OMF.CustomerManagementService.Command.Application.Event;
using OMF.CustomerManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Application
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
            builder.RegisterType<CreateUserCommandHandler>().As<ICommandHandler<CreateUserCommand>>();
            builder.RegisterType<DeleteUserCommandHandler>().As<ICommandHandler<DeleteUserCommand>>();
        }
    }
}
