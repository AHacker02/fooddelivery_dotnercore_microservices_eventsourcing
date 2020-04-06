using Autofac;
using BaseService;
using DataAccess.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.CustomerManagementService.Command.Application;
using OMF.CustomerManagementService.Command.Application.Command;
using OMF.CustomerManagementService.Command.Application.Event;
using ServiceBus.Abstractions;
using ServiceBus.RabbitMq;

namespace OMF.CustomerManagementService.Command
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMq(Configuration);
            base.ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Customer API",
                Description = "Customer management service API"
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MongoDbModule(Configuration));
            builder.RegisterModule(new CustomerModule(Configuration));
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILifetimeScope container)
        {
            base.ConfigureApplication(app, env);
            var eventBus = container.Resolve<IEventBus>();
            eventBus.SubscribeCommand<CreateUserCommand>();
            eventBus.SubscribeCommand<DeleteUserCommand>();
        }
    }
}
