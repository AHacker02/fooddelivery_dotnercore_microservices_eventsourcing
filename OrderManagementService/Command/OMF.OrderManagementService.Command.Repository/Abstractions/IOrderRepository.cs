using System;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Command.Repository.Abstractions
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task<Order> GetOrder(Guid orderId);
        Task UpdateOrder(Order order);
    }
}
