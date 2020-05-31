using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Enums;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.EventHandlers
{
    public class PaymentEventHandler : IEventHandler<PaymentInitiatedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _bus;
        private readonly IMapper _map;

        public PaymentEventHandler(IOrderRepository orderRepository, IEventBus bus,IMapper map)
        {
            _orderRepository = orderRepository;
            _bus = bus;
            _map = map;
        }

        public async Task HandleAsync(PaymentInitiatedEvent @event)
        {
            try
            {

                if (@event.Domain == "Food")
                    await OrderPaymentProcessing(@event.OrderId, @event.Id);
                else if (@event.Domain == "Table")
                    await TablePaymentProcessing(@event.OrderId);


            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }

        private async Task OrderPaymentProcessing(int id,Guid eventId)
        {
            var order = await _orderRepository.GetDetails<TblFoodOrder>(id);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (order.Status == OrderStatus.PaymentPending.ToString() && timer.ElapsedMilliseconds <= 300000)
            {
                order.Status = (await _orderRepository.GetDetails<TblFoodOrder>(order.Id)).Status;
            }
            timer.Stop();

            if (order.Status == OrderStatus.PaymentSuccessful.ToString())
            {
                order.Status = OrderStatus.OrderPlaced.ToString();
                await _orderRepository.UpdateOrder(order);
                await _bus.PublishEvent(new OrderConfirmedEvent(eventId, order.TblRestaurantId, _map.Map<List<FoodOrderItem>>(order.TblFoodOrderItem), order.Address));
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed.ToString();
                await _orderRepository.UpdateOrder(order);
            }
        }

        private async Task TablePaymentProcessing(int id)
        {
            var order = await _orderRepository.GetDetails<TblTableBooking>(id);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (order.Status == OrderStatus.PaymentPending.ToString() && timer.ElapsedMilliseconds <= 300000)
            {
                order.Status = (await _orderRepository.GetDetails<TblTableBooking>(order.Id)).Status;
            }
            timer.Stop();

            if (order.Status == OrderStatus.PaymentSuccessful.ToString())
            {
                order.Status = OrderStatus.OrderPlaced.ToString();
                await _orderRepository.UpdateDetails(order);
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed.ToString();
                await _orderRepository.UpdateDetails(order);
            }
        }
    }
}