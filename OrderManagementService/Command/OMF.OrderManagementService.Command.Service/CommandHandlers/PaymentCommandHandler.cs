using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Events;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class PaymentCommandHandler:ICommandHandler<PaymentCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _map;
        private readonly IEventBus _bus;

        public PaymentCommandHandler(IOrderRepository orderRepository,IMapper map,IEventBus bus)
        {
            _orderRepository = orderRepository;
            _map = map;
            _bus = bus;
        }
        public async Task HandleAsync(PaymentCommand command)
        {
            try
            {
                var transactionId=await _orderRepository.CreatePayment(_map.Map<TblOrderPayment>(command));

                if (transactionId != default && command.Domain=="Food")
                {
                    var order = await _orderRepository.GetDetails<TblFoodOrder>(command.OrderId);
                    await _bus.PublishEvent(new UpdateStockEvent(command.Id,order.TblRestaurantId,_map.Map<IEnumerable<FoodOrderItem>>(order.TblFoodOrderItem)));
                }
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}