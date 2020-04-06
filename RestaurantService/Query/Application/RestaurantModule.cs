using Autofac;
using OMF.RestaurantService.Query.Application.Repositories;
using OMF.RestaurantService.Query.Application.Services;

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
