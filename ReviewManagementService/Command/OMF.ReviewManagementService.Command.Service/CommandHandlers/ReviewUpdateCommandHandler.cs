using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.ReviewManagementService.Command.Repository.Abstractions;
using OMF.ReviewManagementService.Command.Repository.DataContext;
using OMF.ReviewManagementService.Command.Service.Command;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Command.Service.CommandHandlers
{
    public class ReviewUpdateCommandHandler : IRequestHandler<ReviewCommand, Response>
    {
        private readonly IEventBus _bus;
        private readonly IMapper _map;
        private readonly IReviewRepository _reviewRepository;

        public ReviewUpdateCommandHandler(IReviewRepository reviewRepository, IEventBus bus, IMapper map)
        {
            _reviewRepository = reviewRepository;
            _bus = bus;
            _map = map;
        }

        public async Task<Response> Handle(ReviewCommand command, CancellationToken cancellationToken)
        {

            var review = _map.Map<TblRating>(command);
            await _reviewRepository.UpsertReview(review);

            var rating=(await _reviewRepository.GetRestaurantReviews(command.RestaurantId)).Average(x => Convert.ToDecimal(x.Rating));

            await _bus.PublishEvent(new UpdateRestaurantEvent(command.RestaurantId, rating.ToString()));

            return new Response(200,"Review added successfully");

        }
    }
}