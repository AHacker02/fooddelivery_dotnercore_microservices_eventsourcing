using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Application;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Events;
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
            services.AddDbContext<OrderManagementContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString:SqlServer"],
                    x => x.MigrationsAssembly("OMF.OrderManagementService.Command.Repository")));
            ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Order API",
                Description = "Order management service API"
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new OrderModule());
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILifetimeScope container)
        {
            ConfigureApplication(app, env);
            var context = container.Resolve<OrderManagementContext>();
            context.Database.EnsureCreated();
            var eventBus = container.Resolve<IEventBus>();

            //start listening to events
            eventBus.SubscribeEvent<ItemOutOfStockEvent>();
            eventBus.SubscribeEvent<ItemPriceUpdateEvent>();
            eventBus.SubscribeEvent<PaymentInitiatedEvent>();
        }
    }
}