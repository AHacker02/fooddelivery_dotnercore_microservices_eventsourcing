using Autofac;
using BaseService;
using DataAccess.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.ReviewManagementService.Query.Application;

namespace OMF.ReviewManagementService.Query
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
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



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.ConfigureApplication(app, env);
        }
    }
}
