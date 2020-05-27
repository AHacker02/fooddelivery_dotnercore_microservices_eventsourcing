using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Enums;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class TableBookingCommandHandler : ICommandHandler<TableBookingCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _map;
        private readonly IEventBus _bus;

        public TableBookingCommandHandler(IOrderRepository orderRepository, IMapper map, IEventBus bus)
        {
            _orderRepository = orderRepository;
            _map = map;
            _bus = bus;
        }

        public async Task HandleAsync(TableBookingCommand command)
        {
            try
            {
                var booking = _map.Map<TblTableBooking>(command);

                booking.Status = OrderStatus.PaymentPending.ToString();
                //if(await _orderRepository.CheckAvailibility(command.))
                
                await _orderRepository.CreateBooking(booking);
                await _bus.PublishEvent(new PaymentInitiatedEvent(command.Id, booking.Id, "Table"));
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}
