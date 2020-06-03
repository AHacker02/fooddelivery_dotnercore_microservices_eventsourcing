using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OMF.Common.Abstractions;
using OMF.Common.Enums;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;

namespace OMF.OrderManagementService.Command.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly OrderManagementContext _database;
        private readonly IHttpWrapper _httpWrapper;

        public OrderRepository(OrderManagementContext database, IHttpWrapper httpWrapper, IConfiguration configuration)
        {
            _database = database;
            _httpWrapper = httpWrapper;
            _configuration = configuration;
        }

        public async Task<TblFoodOrder> CreateOrder(TblFoodOrder order)
        {
            _database.TblFoodOrder.Add(order);
            return await _database.SaveChangesAsync() > 0 ? order : null;
        }

        public async Task<TblFoodOrder> UpdateOrder(TblFoodOrder order)
        {
            var dborder = await _database.TblFoodOrder.FirstOrDefaultAsync(x => x.Id == order.Id);
            dborder.Copy(order);
            order.ModifiedDate = DateTime.UtcNow;
            return await _database.SaveChangesAsync() > 0 ? order : null;
        }

        public bool CheckAvailibility(int restaurantId, DateTime fromDate, DateTime toDate, ref Restaurant restaurant)
        {
            var restaurantTask =
                _httpWrapper.Get<IEnumerable<Restaurant>>(string.Format(_configuration["RestaurantURL"], restaurantId));
            var bookedTable = _database.TblTableBooking.CountAsync(x => x.FromDate >= fromDate && x.ToDate >= toDate);
            Task.WhenAll(restaurantTask, bookedTable);
            restaurant = restaurantTask.Result.FirstOrDefault();
            return restaurant.RestaurantDetails.FirstOrDefault().TableCount - bookedTable.Result > 0;
        }

        public async Task UpdatePrice(ItemPriceUpdateEvent @event)
        {
            await _database.TblFoodOrderItem.Where(x => x.TblMenuId == @event.ItemId)
                .ForEachAsync(x => x.Price = @event.Price);
            await _database.SaveChangesAsync();
        }

        public async Task OrderOutOfStock(ItemOutOfStockEvent @event)
        {
            await _database.TblFoodOrderItem.Where(x => x.TblMenuId == @event.ItemId && x.TblFoodOrder.PaymentId == 0).ForEachAsync(x => x.Quantity = 0);
            await _database.SaveChangesAsync();
        }

        public async Task<T> GetDetails<T>(int id, Expression<Func<T, object>> include = null) where T : class, IEntity
        {
            return Include(_database.Set<T>(), include).FirstOrDefault(x => x.Id == id);
        }

        public async Task UpdateDetails<T>(T order) where T : class, IEntity
        {
            var dborder = await _database.Set<T>().FirstOrDefaultAsync(x => x.Id == order.Id);
            dborder.Copy(order);
            await _database.SaveChangesAsync();
        }

        public async Task<Guid> CreatePayment(TblOrderPayment payment, string domain, int orderId)
        {
            var transactionId = Guid.NewGuid();
            payment.TransactionId = transactionId;
            await _database.TblOrderPayment.AddAsync(payment);
            await _database.SaveChangesAsync();
            if (domain == Domain.Food.ToString())
            {
                var order = await _database.TblFoodOrder.FirstOrDefaultAsync(x => x.Id == orderId);
                order.Status = OrderStatus.PaymentSuccessful.ToString();
                order.PaymentId = payment.Id;
            }
            else if (domain == Domain.Table.ToString())
            {
                var order = await _database.TblTableBooking.FirstOrDefaultAsync(x => x.Id == orderId);
                order.Status = OrderStatus.PaymentSuccessful.ToString();
                order.PaymentId = payment.Id;
            }

            await _database.SaveChangesAsync();

            return transactionId;
        }

        public async Task<TblTableBooking> CreateBooking(TblTableBooking booking)
        {
            _database.TblTableBooking.Add(booking);
            return await _database.SaveChangesAsync() > 0 ? booking : null;
        }

        public IEnumerable<TEntity> Include<TEntity>(DbSet<TEntity> dbSet, Expression<Func<TEntity, object>> include)
            where TEntity : class
        {
            if (include == null) return dbSet;

            IEnumerable<TEntity> query = null;

            query = dbSet.Include(include);


            return query ?? dbSet;
        }
        
        
        //Generic methods
        public async Task<IQueryable<TEntity>> GetAll<TEntity>() where TEntity : class
            => _database.Set<TEntity>().AsNoTracking();
 
        public async Task<IQueryable<TEntity>> Get<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
            => _database.Set<TEntity>().Where(condition).AsNoTracking();
 
        public async Task Create<TEntity>(TEntity entity) where TEntity : class
        {
            await _database.Set<TEntity>().AddAsync(entity);
            await _database.SaveChangesAsync();
        }
 
        public async Task Update<TEntity>(TEntity entity) where TEntity : class
        {
            _database.Set<TEntity>().Update(entity);
            await _database.SaveChangesAsync();
        }
 
        public async Task Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _database.Set<TEntity>().Remove(entity);
            await _database.SaveChangesAsync();
        }
        
    }
}