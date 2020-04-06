using OMF.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.OrderManagementService.Query.Application.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
    }
}
