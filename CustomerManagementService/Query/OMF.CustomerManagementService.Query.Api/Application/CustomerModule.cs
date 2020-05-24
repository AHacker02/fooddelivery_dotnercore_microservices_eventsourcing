using Autofac;
using Microsoft.Extensions.Configuration;
using OMF.CustomerManagementService.Query.Repository;
using OMF.CustomerManagementService.Query.Repository.Abstractions;
using OMF.CustomerManagementService.Query.Repository.DataContext;
using OMF.CustomerManagementService.Query.Service;
using OMF.CustomerManagementService.Query.Service.Abstractions;

namespace OMF.CustomerManagementService.Query.Api.Application
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
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<AuthRepository>().As<IAuthRepository>();
            builder.Register(c => new CustomerManagementContext(_configuration["ConnectionString:SqlServer"]));
        }
    }
}
