using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Query.Repository.Abstractions
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
    }
}