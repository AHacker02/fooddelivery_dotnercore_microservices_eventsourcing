using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.Common.Events;
using OMF.RestaurantService.Command.Application;
using OMF.RestaurantService.Command.Service;
using ServiceBus.Abstractions;
using ServiceBus.RabbitMq;

namespace OMF.RestaurantService.Command
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMq(Configuration);
            base.ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Restaurant API",
                Description = "Restaurant management service API"
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RestaurantModule());
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder, ILifetimeScope container)
        {
            base.ConfigureApplication(app, env);
            seeder.SeedRestaurants();
            var eventBus = container.Resolve<IEventBus>();
            eventBus.SubscribeEvent<OrderConfirmedEvent>();
            eventBus.SubscribeEvent<UpdateRestaurantEvent>();
        }
    }
}
