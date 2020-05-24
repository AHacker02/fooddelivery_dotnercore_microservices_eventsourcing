using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.CustomerManagementService.Command.Api.Application;
using OMF.CustomerManagementService.Command.Repository.DataContext;
using OMF.CustomerManagementService.Command.Service.Command;
using ServiceBus.Abstractions;
using ServiceBus.RabbitMq;

namespace OMF.CustomerManagementService.Command.Api
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMq(Configuration);
            ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Customer API",
                Description = "Customer management service API"
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new CustomerModule(Configuration));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILifetimeScope container)
        {
            ConfigureApplication(app, env);

            var context = container.Resolve<CustomerManagementContext>();
            context.Database.EnsureCreated();

            var eventBus = container.Resolve<IEventBus>();
            eventBus.SubscribeCommand<CreateUserCommand>();
            eventBus.SubscribeCommand<DeleteUserCommand>();
            eventBus.SubscribeCommand<UpdateUserCommand>();
        }
    }
}