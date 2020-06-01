using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Query.Repository.Abstractions
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetUserOrders(int userId);
        Task<IEnumerable<Booking>> GetUserBookings(int userId);
        Task<IEnumerable<Payment>> GetUserTransactions(int userId);
    }
}