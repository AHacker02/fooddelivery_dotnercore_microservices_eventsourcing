using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMF.Common.Models;
using OMF.OrderManagementService.Query.Repository.Abstractions;
using OMF.OrderManagementService.Query.Repository.DataContext;

namespace OMF.OrderManagementService.Query.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagementContext _database;
        private readonly IMapper _map;

        public OrderRepository(OrderManagementContext database, IMapper map)
        {
            _database = database;
            _map = map;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(int userId)
            => _map.Map<IEnumerable<Order>>(_database.TblFoodOrder.Include(x => x.TblFoodOrderItem)
                .Where(x => x.TblCustomerId == userId));

        public async Task<IEnumerable<Booking>> GetUserBookings(int userId)
            => _map.Map<IEnumerable<Booking>>(_database.TblTableBooking.Include(x=>x.TblTableDetail).Where(x => x.TblCustomerId == userId));

        public async Task<IEnumerable<Payment>> GetUserTransactions(int userId)
            => _map.Map<IEnumerable<Payment>>(_database.TblOrderPayment.Where(x => x.TblCustomerId == userId));

    }


}
