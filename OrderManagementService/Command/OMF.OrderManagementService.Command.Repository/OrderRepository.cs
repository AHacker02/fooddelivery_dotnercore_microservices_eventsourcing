using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OMF.Common.Abstractions;
using OMF.Common.Enums;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;

namespace OMF.OrderManagementService.Command.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagementContext _database;
        private readonly IHttpWrapper _httpWrapper;
        private readonly IConfiguration _configuration;

        public OrderRepository(OrderManagementContext database,IHttpWrapper httpWrapper,IConfiguration configuration)
        {
            _database = database;
            _httpWrapper = httpWrapper;
            _configuration = configuration;
        }

        public async Task<TblFoodOrder> CreateOrder(TblFoodOrder order)
        {
            order.CreatedDate=DateTime.UtcNow;
            _database.TblFoodOrder.Add(order);
            return await _database.SaveChangesAsync()>0 ? order : null;
        }

        public async Task<TblFoodOrder> UpdateOrder(TblFoodOrder order)
        {
            var dborder =await _database.TblFoodOrder.FirstOrDefaultAsync(x => x.Id == order.Id);
            dborder.Copy(order);
            order.ModifiedDate = DateTime.UtcNow;
            _database.SaveChanges();
            return await _database.SaveChangesAsync() > 0 ? order : null;
        }

        public bool CheckAvailibility(int restaurantId, DateTime fromDate, DateTime toDate,ref Restaurant restaurant)
        {
            var restaurantTask = _httpWrapper.Get<Restaurant>(string.Format(_configuration["RestaurantURL"], restaurantId));
            var bookedTable = _database.TblTableBooking.CountAsync(x => x.FromDate >= fromDate && x.ToDate >= toDate);
            Task.WhenAll(restaurantTask, bookedTable);
            restaurant = restaurantTask.Result;
            return restaurant.RestaurantDetails.FirstOrDefault().TableCount - bookedTable.Result > 0;
        }

        public async Task<T> GetDetails<T>(int id) where T :class, IEntity
            => await _database.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateDetails<T>(T order) where T : class, IEntity
        {
            var dborder=await _database.Set<T>().FirstOrDefaultAsync(x => x.Id == order.Id);
            dborder.Copy(order);
            await _database.SaveChangesAsync();
        }

        public async Task<Guid> CreatePayment(TblOrderPayment payment, string domain, int orderId)
        {
            var trasactionId=Guid.NewGuid();
            payment.TransactionId = trasactionId;
            _database.TblOrderPayment.Add(payment);
            if (domain == Domain.Food.ToString())
            {
                (await _database.TblFoodOrder.FirstOrDefaultAsync(x => x.Id == orderId)).Status =
                    OrderStatus.PaymentSuccessful.ToString();
            }
            else if (domain == Domain.Table.ToString())
            {
                (await _database.TblTableBooking.FirstOrDefaultAsync(x => x.Id == orderId)).Status =
                    OrderStatus.PaymentSuccessful.ToString();
            }

            await _database.SaveChangesAsync();

            return trasactionId;
        }

        public async Task<TblTableBooking> CreateBooking(TblTableBooking booking)
        {
            _database.TblTableBooking.Add(booking);
            return await _database.SaveChangesAsync() > 0 ? booking : null;
        }

        
    }
}
