using MediatR;
using OMF.Common.Models;

namespace OMF.ReviewManagementService.Command.Service.Command
{
    public class ReviewCommand : IRequest<Response>
    {
        public string Rating { get; set; }
        public string Comments { get; set; }
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
    }
}