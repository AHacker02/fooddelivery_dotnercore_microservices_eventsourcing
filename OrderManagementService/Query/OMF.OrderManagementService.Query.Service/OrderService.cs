using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Order>> GetUserOrders(int userId)
            => (await _orderRepository.GetUserOrders(userId)).Where(x => x.PaymentId != 0);
        
        public async Task<IEnumerable<Order>> GetUserOrdersById(int userId,int orderId)
            => (await _orderRepository.GetUserOrders(userId)).Where(x=>x.Id==orderId && x.PaymentId != 0);
        
        public async Task<IEnumerable<Booking>> GetUserBookings(int userId)
            => await _orderRepository.GetUserBookings(userId);
        
        public async Task<IEnumerable<Booking>> GetUserBookingsById(int userId,int bookingId)
            => (await _orderRepository.GetUserBookings(userId)).Where(x=>x.Id==bookingId);

        public async Task<IEnumerable<Payment>> GetUserTransactions(int userId)
            => await _orderRepository.GetUserTransactions(userId);
        
        public async Task<IEnumerable<Order>> GetUserCart(int userId)
        => (await _orderRepository.GetUserOrders(userId)).Where(x=>x.PaymentId==0);
    }
}
