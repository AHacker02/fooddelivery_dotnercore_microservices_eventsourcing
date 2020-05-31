using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.RestaurantService.Command.Service.Command;
using OMF.RestaurantService.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.RestaurantService.Command.Service.CommandHandlers
{
    public class PriceUpdateCommandHandler:IRequestHandler<PriceUpdateCommand,Response>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEventBus _bus;

        public PriceUpdateCommandHandler(IRestaurantRepository restaurantRepository,IEventBus bus)
        {
            _restaurantRepository = restaurantRepository;
            _bus = bus;
        }
        public async Task<Response> Handle(PriceUpdateCommand request, CancellationToken cancellationToken)
        {
            if(!await _restaurantRepository.UpdatePriceAsync(request.MenuId,request.Price))
                return new Response(400,"Item not found");

            await _bus.PublishEvent(new ItemPriceUpdateEvent(request.MenuId, request.Price));
            return new Response(200,"Item price updated successfully");
        }
    }
}
