using System;
using System.Threading.Tasks;
using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
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

        public OrderRepository(OrderManagementContext database)
        {
            _database = database;
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

        public async Task<T> GetDetails<T>(int id) where T :class, IPaymentEntity
            => await _database.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateOrder<T>(T order) where T : class, IPaymentEntity
        {
            var dborder=await _database.Set<T>().FirstOrDefaultAsync(x => x.Id == order.Id);
            dborder.Copy(order);
            await _database.SaveChangesAsync();
        }

        public async Task<Guid> CreatePayment(TblOrderPayment payment)
        {
            var trasactionId=Guid.NewGuid();
            payment.TransactionId = trasactionId;
            _database.TblOrderPayment.Add(payment);
            if (payment.Domain == "Food")
            {
                (await _database.TblFoodOrder.FirstOrDefaultAsync(x => x.Id == payment.OrderId)).Status =
                    OrderStatus.PaymentSuccessful.ToString();
            }
            else if (payment.Domain == "Table")
            {
                (await _database.TblTableBooking.FirstOrDefaultAsync(x => x.Id == payment.OrderId)).Status =
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
