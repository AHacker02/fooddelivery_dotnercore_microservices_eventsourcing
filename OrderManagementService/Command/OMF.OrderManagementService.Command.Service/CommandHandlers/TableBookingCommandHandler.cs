using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OMF.Common.Enums;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;
using OMF.OrderManagementService.Command.Service.Events;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.CommandHandlers
{
    public class TableBookingCommandHandler : IRequestHandler<TableBookingCommand, Response>,
        IRequestHandler<BookingUpdateCommand, Response>
    {
        private readonly IEventBus _bus;
        private readonly IMapper _map;
        private readonly IOrderRepository _orderRepository;

        public TableBookingCommandHandler(IOrderRepository orderRepository, IMapper map, IEventBus bus)
        {
            _orderRepository = orderRepository;
            _map = map;
            _bus = bus;
        }

        /// <summary>
        /// Handler update table booking
        /// To update member quantity and timing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response</returns>
        public async Task<Response> Handle(BookingUpdateCommand request, CancellationToken cancellationToken)
        {
            var booking = (await _orderRepository.Get<TblTableBooking>(x=>x.Id==request.BookingId)).FirstOrDefault();

            if (booking == null)
            {
                return new Response(400,"Booking not found");
            }

            if (booking.PaymentId == 0 && request.MemberCount != 0)
            {
                return new Response(400,"Table cannot be updated after payment");
            }
            
            Restaurant restaurant = null;
            if (_orderRepository.CheckAvailibility(
                booking.TblRestaurantId,
                request.FromDate ?? booking.FromDate,
                request.ToDate ?? booking.ToDate,
                ref restaurant))
            {
                booking.TblTableDetail.FirstOrDefault().TableNo = (int) Math.Ceiling(
                    Convert.ToDecimal(request.MemberCount) /
                    Convert.ToDecimal(restaurant.RestaurantDetails.FirstOrDefault().TableCapacity));
                booking.FromDate = request.FromDate ?? booking.FromDate;
                booking.ToDate = request.ToDate ?? booking.ToDate;
            }

            await _orderRepository.Update(booking);

            return new Response(200, $"Table booking successful. Booking Id: {booking.Id} ");
        }

        
        /// <summary>
        /// Handler create table booking
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response</returns>
        public async Task<Response> Handle(TableBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = _map.Map<TblTableBooking>(command);

            booking.Status = OrderStatus.PaymentPending.ToString();
            Restaurant restaurant = null;
            if (_orderRepository.CheckAvailibility(command.RestaurantId, command.FromDate, command.ToDate,
                ref restaurant))
            {
                var tableCount = (int) Math.Ceiling(Convert.ToDecimal(command.MemberCount) /
                                                    Convert.ToDecimal(restaurant.RestaurantDetails.FirstOrDefault()
                                                        .TableCapacity));
                booking.TblTableDetail.Add(new TblTableDetail
                {
                    TableNo = tableCount,
                    Price = 500*tableCount
                });
                await _orderRepository.Create(booking);
                await _bus.PublishEvent(new PaymentInitiatedEvent(booking.Id, "Table"));

                return new Response(200, $"Table booking successful. Booking Id: {booking.Id} ");
            }

            return new Response(400, "Booking unavailable");
        }
    }
}