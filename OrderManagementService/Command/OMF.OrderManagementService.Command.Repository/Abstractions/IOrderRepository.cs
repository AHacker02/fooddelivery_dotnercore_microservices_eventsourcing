using System;
using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.DataContext;

namespace OMF.OrderManagementService.Command.Repository.Abstractions
{
    public interface IOrderRepository
    {
        Task<TblFoodOrder> CreateOrder(TblFoodOrder order);
        Task<T> GetDetails<T>(int id) where T : class, IPaymentEntity;
        Task UpdateOrder<T>(T order) where T : class, IPaymentEntity;
        Task<Guid> CreatePayment(TblOrderPayment payment);
        Task<TblTableBooking> CreateBooking(TblTableBooking booking);
        Task<TblFoodOrder> UpdateOrder(TblFoodOrder order);
    }
}
