using OMF.Common.Models;
using OMF.OrderManagementService.Query.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.OrderManagementService.Query.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(Guid userId)
            => await _orderRepository.GetUserOrders(userId);
    }
}
