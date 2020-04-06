using OMF.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.OrderManagementService.Query.Application.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
    }
}
