using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.OrderManagementService.Query.Application;
using OMF.OrderManagementService.Query.Repository.DataContext;

namespace OMF.OrderManagementService.Query
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OrderManagementContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString:SqlServer"]));
            base.ConfigureApplicationServices(services, new OpenApiInfo
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



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.ConfigureApplication(app, env);
        }
    }
}
