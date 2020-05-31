using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OMF.Common.Enums;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand,Response>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _bus;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IEventBus bus)
        {
            _orderRepository = orderRepository;
            _bus = bus;
        }

        public async Task<Response> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            dynamic order = null;
            if (request.Domain == Domain.Food.ToString())
            {
                order= await _orderRepository.GetDetails<TblFoodOrder>(request.OrderId);
                
            }
            else if (request.Domain == Domain.Table.ToString())
            {
                order = await _orderRepository.GetDetails<TblFoodOrder>(request.OrderId);
            }

            if(order==null)
                return new Response(400,"Order not found");

            var payment = (TblOrderPayment) await _orderRepository.GetDetails<TblOrderPayment>(order.PaymentId);
            order.Status = OrderStatus.Cancelled.ToString();
            payment.PaymentStatus = PaymentStatus.Refund.ToString();
            await _orderRepository.UpdateDetails(order);
            await _orderRepository.UpdateDetails(payment);
            return new Response(200,"Payment refund successful");
        }

    }
}
