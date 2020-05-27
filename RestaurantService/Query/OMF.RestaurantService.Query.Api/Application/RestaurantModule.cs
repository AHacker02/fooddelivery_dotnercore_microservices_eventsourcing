using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Nest;
using OMF.RestaurantService.Query.Repository;
using OMF.RestaurantService.Query.Repository.Abstractions;
using OMF.RestaurantService.Query.Repository.DataContext;
using OMF.RestaurantService.Query.Service;
using OMF.RestaurantService.Query.Service.Abstractions;

namespace OMF.RestaurantService.Query.Application
{
    public class RestaurantModule : Module
    {
        private readonly IConfiguration _configuration;

        public RestaurantModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>();
            builder.RegisterType<SearchService>().As<ISearchService>();
            builder.Register<ConnectionSettings>(con =>
                new ConnectionSettings(new Uri(_configuration["ConnectionString:Elastic"])));
            builder.Register<IElasticClient>(c =>
            {
                var con = c.Resolve<ConnectionSettings>();
                return new ElasticClient(con);
            });
            //builder.Register<RestaurantManagementContext>(x =>
            //    new RestaurantManagementContext(_configuration["ConnectionString:SqlServer"]));


        }
    }
}