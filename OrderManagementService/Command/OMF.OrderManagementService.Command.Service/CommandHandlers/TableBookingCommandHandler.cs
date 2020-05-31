using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OMF.Common.Enums;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class TableBookingCommandHandler : IRequestHandler<TableBookingCommand, Response>,IRequestHandler<BookingUpdateCommand,Response>
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

        public async Task<Response> Handle(TableBookingCommand command, CancellationToken cancellationToken)
        {
            
                var booking = _map.Map<TblTableBooking>(command);

                booking.Status = OrderStatus.PaymentPending.ToString();
                Restaurant restaurant = null;
                if (_orderRepository.CheckAvailibility(command.RestaurantId, command.FromDate, command.ToDate,ref restaurant))
                {
                    booking.TblTableDetail.Add(new TblTableDetail()
                    {
                        TableNo = (int)Math.Ceiling(Convert.ToDecimal(command.MemberCount)/Convert.ToDecimal(restaurant.RestaurantDetails.FirstOrDefault().TableCapacity))
                    });
                    booking = await _orderRepository.CreateBooking(booking);
                    await _bus.PublishEvent(new PaymentInitiatedEvent(booking.Id, "Table"));

                    return new Response(200, $"Table booking successful. Booking Id: {booking.Id} ");
                }
                return new Response(400,"Booking unavailable");
                
                
           
        }

        public async Task<Response> Handle(BookingUpdateCommand request, CancellationToken cancellationToken)
        {
            var booking = await _orderRepository.GetDetails<TblTableBooking>(request.BookingId);
            Restaurant restaurant = null;
            if (_orderRepository.CheckAvailibility(booking.TblRestaurantId, request.FromDate, request.ToDate,
                ref restaurant))
            {
                booking.TblTableDetail.FirstOrDefault().TableNo = (int)Math.Ceiling(Convert.ToDecimal(request.MemberCount) / Convert.ToDecimal(restaurant.RestaurantDetails.FirstOrDefault().TableCapacity));
                booking.FromDate = request.FromDate;
                booking.ToDate = request.ToDate;
            }

            return new Response(200, $"Table booking successful. Booking Id: {booking.Id} ");
        }
    }
}
