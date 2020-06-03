using MediatR;
using OMF.Common.Models;

namespace OMF.RestaurantService.Command.Service.Command
{
    public class PriceUpdateCommand : IRequest<Response>
    {
        public int MenuId { get; set; }
        public decimal Price { get; set; }
    }
}
