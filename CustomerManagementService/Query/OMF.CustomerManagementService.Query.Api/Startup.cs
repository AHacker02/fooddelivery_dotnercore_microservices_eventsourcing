using Autofac;
using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMF.CustomerManagementService.Query.Api.Application;
using OMF.CustomerManagementService.Query.Repository.DataContext;

namespace OMF.CustomerManagementService.Query.Api
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
                Title = "Customer API",
                Description = "Customer management service API"
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new CustomerModule(Configuration));
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILifetimeScope container)
        {
            base.ConfigureApplication(app, env);
            
            var context = container.Resolve<CustomerManagementContext>();
            context.Database.EnsureCreated();
        }
    }
}
