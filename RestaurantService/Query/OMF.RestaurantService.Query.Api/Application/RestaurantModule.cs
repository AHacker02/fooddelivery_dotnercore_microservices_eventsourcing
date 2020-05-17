using Autofac;
using OMF.RestaurantService.Query.Repository;
using OMF.RestaurantService.Query.Repository.Abstractions;
using OMF.RestaurantService.Query.Service;
using OMF.RestaurantService.Query.Service.Abstractions;

namespace OMF.RestaurantService.Query.Application
{
    public class RestaurantModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>();
            builder.RegisterType<SearchService>().As<ISearchService>();
        }
    }
}