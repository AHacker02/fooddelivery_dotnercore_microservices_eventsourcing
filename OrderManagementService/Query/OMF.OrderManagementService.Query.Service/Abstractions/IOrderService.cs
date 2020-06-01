using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Query.Service.Abstractions
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(int userId);
        Task<IEnumerable<Payment>> GetUserTransactions(int userId);
        Task<IEnumerable<Order>> GetUserOrdersById(int userId, int orderId);
        Task<IEnumerable<Booking>> GetUserBookings(int userId);
        Task<IEnumerable<Booking>> GetUserBookingsById(int userId, int bookingId);
        Task<IEnumerable<Order>> GetUserCart(int userId);
    }
}
