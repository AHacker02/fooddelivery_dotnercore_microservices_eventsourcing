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
using OMF.OrderManagementService.Command.Service.Commands;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class OrderCommandHandler : ICommandHandler<OrderCommand>
    {
        private readonly IEventBus _bus;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _map;

        public OrderCommandHandler(IEventBus bus, IOrderRepository orderRepository, IMapper map)
        {
            _bus = bus;
            _orderRepository = orderRepository;
            _map = map;
        }

        public async Task HandleAsync(OrderCommand command)
        {
            try
            {
                var order = await _orderRepository.CreateOrder(_map.Map<TblFoodOrder>(command));
                if (command.BookNow)
                {
                    order.Status = OrderStatus.PaymentPending.ToString();
                }
                foreach (var item in command.OrderItems)
                {
                    order.TblFoodOrderItem.Add(new TblFoodOrderItem()
                    {
                        TblFoodOrderId = order.Id,
                        TblMenuId = item.MenuId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        CreatedDate = DateTime.UtcNow
                    });
                }

                await _orderRepository.UpdateOrder(order);
                if(command.BookNow)
                    await _bus.PublishEvent(new PaymentInitiatedEvent(command.Id,order.Id, "Food"));
                
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}