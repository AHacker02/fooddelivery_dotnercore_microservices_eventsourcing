using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.DataContext;

namespace OMF.OrderManagementService.Command.Repository.Abstractions
{
    public interface IOrderRepository
    {
        Task<TblFoodOrder> CreateOrder(TblFoodOrder order);
        Task<T> GetDetails<T>(int id, Expression<Func<T, object>> include = null) where T : class, IEntity;
        Task UpdateDetails<T>(T order) where T : class, IEntity;
        Task<Guid> CreatePayment(TblOrderPayment payment, string domain, int orderId);
        Task<TblTableBooking> CreateBooking(TblTableBooking booking);
        Task<TblFoodOrder> UpdateOrder(TblFoodOrder order);
        bool CheckAvailibility(int restaurantId, DateTime fromDate, DateTime toDate, ref Restaurant restaurant);
        Task UpdatePrice(ItemPriceUpdateEvent @event);
        Task OrderOutOfStock(ItemOutOfStockEvent @event);
        
        //Generics
        Task<IQueryable<TEntity>> GetAll<TEntity>() where TEntity : class;
        Task<IQueryable<TEntity>> Get<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;
        Task Create<TEntity>(TEntity entity) where TEntity : class;
        Task Update<TEntity>(TEntity entity) where TEntity : class;
        Task Delete<TEntity>(TEntity entity) where TEntity : class;
    }
}