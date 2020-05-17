using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Query.Service.Abstractions
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
    }
}
