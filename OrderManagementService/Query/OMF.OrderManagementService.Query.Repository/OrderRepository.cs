using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Abstractions;
using OMF.Common.Models;
using OMF.OrderManagementService.Query.Repository.Abstractions;

namespace OMF.OrderManagementService.Query.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly INoSqlDataAccess _database;

        public OrderRepository(INoSqlDataAccess database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(Guid userId)
            => await _database.Find<Order>(x => x.UserId == userId);
    }
}
