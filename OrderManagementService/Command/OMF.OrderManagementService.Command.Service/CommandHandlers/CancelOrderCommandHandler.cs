using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OMF.Common.Enums;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Response>
    {
        private readonly IEventBus _bus;
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IEventBus bus)
        {
            _orderRepository = orderRepository;
            _bus = bus;
        }

        
        /// <summary>
        /// Handler cancel order
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Refunded ammount</returns>
        public async Task<Response> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            dynamic order = null;
            if (request.Domain == Domain.Food.ToString())
                order = (await _orderRepository.Get<TblFoodOrder>(x=>x.Id==request.OrderId)).FirstOrDefault();
            else if (request.Domain == Domain.Table.ToString())
                order = (await _orderRepository.Get<TblTableBooking>(x=>x.Id==request.OrderId)).FirstOrDefault();

            if (order == null)
                return new Response(400, "Order not found");
            int paymentId = order.PaymentId;
            var payment = (await _orderRepository.Get<TblOrderPayment>(x=>x.Id==paymentId)).FirstOrDefault();
            order.Status = OrderStatus.Cancelled.ToString();
            payment.PaymentStatus = PaymentStatus.Refund.ToString();
            await _orderRepository.Update(order);
            await _orderRepository.Update(payment);
            return new Response(200, $"Ammount Rs.{payment.TransactionAmount} refunded successfully");
        }
    }
}