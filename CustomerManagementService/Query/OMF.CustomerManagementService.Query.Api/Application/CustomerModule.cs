using Autofac;
using Microsoft.Extensions.Configuration;
using OMF.CustomerManagementService.Query.Application.Repositories;
using OMF.CustomerManagementService.Query.Application.Services;

namespace OMF.CustomerManagementService.Query.Application
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
        }
    }
}
