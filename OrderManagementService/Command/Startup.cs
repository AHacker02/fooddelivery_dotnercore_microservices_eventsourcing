using Autofac;
using BaseService;
using DataAccess.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Application;
using OMF.OrderManagementService.Command.Application.Commands;
using OMF.OrderManagementService.Command.Application.Events;
using ServiceBus.Abstractions;
using ServiceBus.RabbitMq;

namespace OMF.OrderManagementService.Command
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
                Title = "Order API",
                Description = "Order management service API"
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MongoDbModule(Configuration));
            builder.RegisterModule(new OrderModule());
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILifetimeScope container)
        {
            base.ConfigureApplication(app, env);
            var eventBus = container.Resolve<IEventBus>();
            eventBus.SubscribeCommand<CancelOrderCommand>();
            eventBus.SubscribeCommand<OrderCommand>();
            eventBus.SubscribeEvent<OrderReadyEvent>();
            eventBus.SubscribeEvent<PaymentInitiatedEvent>();
        }
    }
}
