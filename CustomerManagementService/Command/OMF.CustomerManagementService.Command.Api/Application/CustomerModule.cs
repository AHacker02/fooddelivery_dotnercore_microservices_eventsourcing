using Autofac;
using Microsoft.Extensions.Configuration;
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
            builder.Register(c => new CustomerManagementContext(_configuration["ConnectionString:SqlServer"]));

            //Handler Registrations
            builder.RegisterType<CreateUserCommandHandler>().As<ICommandHandler<CreateUserCommand>>();
            builder.RegisterType<DeleteUserCommandHandler>().As<ICommandHandler<DeleteUserCommand>>();
            builder.RegisterType<UpdateUserCommandHandler>().As<ICommandHandler<UpdateUserCommand>>();
        }
    }
}