using Autofac;
using BaseService;
using DataAccess.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.ReviewManagementService.Command.Application;
using OMF.ReviewManagementService.Command.Application.Command;
using OMF.ReviewManagementService.Command.Application.Event;
using ServiceBus.Abstractions;
using ServiceBus.RabbitMq;

namespace OMF.ReviewManagementService
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
                Title = "Review API",
                Description = "Review management service API"
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MongoDbModule(Configuration));
            builder.RegisterModule(new ReviewModule());
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILifetimeScope container)
        {
            base.ConfigureApplication(app, env);
            var eventBus = container.Resolve<IEventBus>();
            eventBus.SubscribeCommand<ReviewCommand>();
            eventBus.SubscribeEvent<ReviewCreatedEvent>();
        }
    }
}
