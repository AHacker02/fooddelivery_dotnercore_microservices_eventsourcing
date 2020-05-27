using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.ReviewManagementService.Query.Application;
using OMF.ReviewManagementService.Query.Repository.DataContext;

namespace OMF.ReviewManagementService.Query
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RatingDataContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString:SqlServer"]));
            base.ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Review API",
                Description = "Review management service API"
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ReviewModule(Configuration));
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.ConfigureApplication(app, env);
        }
    }
}
