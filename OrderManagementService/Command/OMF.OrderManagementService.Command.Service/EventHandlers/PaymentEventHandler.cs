using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OMF.Common.Enums;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.EventHandlers
{
    public class PaymentEventHandler : IEventHandler<PaymentInitiatedEvent>
    {
        private readonly IEventBus _bus;
        private readonly IMapper _map;
        private readonly ILogger<PaymentEventHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public PaymentEventHandler(IOrderRepository orderRepository, IEventBus bus, IMapper map,ILogger<PaymentEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _bus = bus;
            _map = map;
            _logger = logger;
        }
        
        
        /// <summary>
        /// Event Handler
        /// To wait for 5min for payment to complete
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
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
                _logger.LogError("System Exception",ex);
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", @event));
            }
        }

        private async Task OrderPaymentProcessing(int id, Guid eventId)
        {
            var order = await _orderRepository.GetDetails<TblFoodOrder>(id);

            var timer = new Stopwatch();
            timer.Start();
            while (order.Status == OrderStatus.PaymentPending.ToString() && timer.ElapsedMilliseconds <= 300000)
            {
                order.Status = (await _orderRepository.GetDetails<TblFoodOrder>(order.Id)).Status;
                Thread.Sleep(60000);
            }
            timer.Stop();

            if (order.Status == OrderStatus.PaymentSuccessful.ToString())
            {
                order.Status = OrderStatus.OrderPlaced.ToString();
                await _orderRepository.UpdateOrder(order);
                await _bus.PublishEvent(new OrderConfirmedEvent(order.TblRestaurantId,
                    _map.Map<List<FoodOrderItem>>(order.TblFoodOrderItem)));
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

            var timer = new Stopwatch();
            timer.Start();
            while (order.Status == OrderStatus.PaymentPending.ToString() && timer.ElapsedMilliseconds <= 300000)
                order.Status = (await _orderRepository.GetDetails<TblTableBooking>(order.Id)).Status;
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