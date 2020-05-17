using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.OrderManagementService.Query.Repository.Abstractions;
using OMF.OrderManagementService.Query.Service.Abstractions;

namespace OMF.OrderManagementService.Query.Service
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
