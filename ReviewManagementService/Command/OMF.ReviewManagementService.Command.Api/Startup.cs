using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.ReviewManagementService.Command.Application;
using OMF.ReviewManagementService.Command.Repository.DataContext;
using OMF.ReviewManagementService.Command.Service.Command;
using ServiceBus.Abstractions;
using ServiceBus.RabbitMq;

namespace OMF.ReviewManagementService.Command
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RatingDataContext>(options =>
                options.UseSqlServer(Configuration.GetSection("ConnectionString").Value));
            services.AddRabbitMq(Configuration);
            ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Review API",
                Description = "Review management service API"
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ReviewModule());
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILifetimeScope container)
        {
            ConfigureApplication(app, env);
            var eventBus = container.Resolve<IEventBus>();
            eventBus.SubscribeCommand<ReviewCommand>();
        }
    }
}