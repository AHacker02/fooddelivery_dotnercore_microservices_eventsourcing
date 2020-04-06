using OMF.Common.Models;
using System;
using System.Threading.Tasks;

namespace OMF.OrderManagementService.Command.Application.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task<Order> GetOrder(Guid orderId);
        Task UpdateOrder(Order order);
    }
}
