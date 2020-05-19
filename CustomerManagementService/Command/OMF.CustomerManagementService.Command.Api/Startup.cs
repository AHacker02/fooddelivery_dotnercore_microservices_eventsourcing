using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<CustomerManagementContext>(options =>
                options.UseSqlServer(Configuration.GetSection("ConnectionString").Value));
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
