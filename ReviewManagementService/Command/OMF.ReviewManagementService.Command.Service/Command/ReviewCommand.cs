using System.ComponentModel.DataAnnotations;
using MediatR;
using OMF.Common.Models;

namespace OMF.ReviewManagementService.Command.Service.Command
{
    public class ReviewCommand : IRequest<Response>
    {
        [RegularExpression("^(?:[1-9]|0[1-9]|10)$")]
        public string Rating { get; set; }
        public string Comments { get; set; }
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
    }
}