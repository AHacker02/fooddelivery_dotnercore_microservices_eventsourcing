using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.RestaurantService.Query.Application;
using OMF.RestaurantService.Query.Repository.DataContext;

namespace OMF.RestaurantService.Query
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RestaurantManagementContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString:SqlServer"]));
            ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Restaurant API",
                Description = "Restaurant management service API"
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RestaurantModule(Configuration));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureApplication(app, env);
        }
    }
}