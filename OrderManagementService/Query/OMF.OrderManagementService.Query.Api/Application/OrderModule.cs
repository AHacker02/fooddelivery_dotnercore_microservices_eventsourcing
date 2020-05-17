using Autofac;
using OMF.OrderManagementService.Query.Repository;
using OMF.OrderManagementService.Query.Repository.Abstractions;
using OMF.OrderManagementService.Query.Service;
using OMF.OrderManagementService.Query.Service.Abstractions;

namespace OMF.OrderManagementService.Query.Application
{
    public class OrderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderService>().As<IOrderService>();
        }
    }
}
