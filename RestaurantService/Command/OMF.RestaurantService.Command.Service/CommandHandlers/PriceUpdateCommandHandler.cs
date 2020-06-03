using MediatR;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.RestaurantService.Command.Service.Command;
using OMF.RestaurantService.Repository.Abstractions;
using ServiceBus.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace OMF.RestaurantService.Command.Service.CommandHandlers
{
    public class PriceUpdateCommandHandler : IRequestHandler<PriceUpdateCommand, Response>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEventBus _bus;

        public PriceUpdateCommandHandler(IRestaurantRepository restaurantRepository, IEventBus bus)
        {
            _restaurantRepository = restaurantRepository;
            _bus = bus;
        }


        /// <summary>
        /// Handler Price update command
        /// To update item price
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response</returns>
        public async Task<Response> Handle(PriceUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!await _restaurantRepository.UpdatePriceAsync(request.MenuId, request.Price))
                return new Response(400, "Item not found");


            //raise itempriceupdate event
            await _bus.PublishEvent(new ItemPriceUpdateEvent(request.MenuId, request.Price));
            return new Response(200, "Item price updated successfully");
        }
    }
}
