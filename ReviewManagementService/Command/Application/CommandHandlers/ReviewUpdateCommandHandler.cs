using AutoMapper;
using OMF.Common.Models;
using OMF.ReviewManagementService.Command.Application.Command;
using OMF.ReviewManagementService.Command.Application.Event;
using OMF.ReviewManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;
using System;
using System.Threading.Tasks;
using OMF.Common.Events;

namespace OMF.ReviewManagementService.Command.Application.CommandHandlers
{
    public class ReviewUpdateCommandHandler : ICommandHandler<ReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IEventBus _bus;
        private readonly IMapper _map;

        public ReviewUpdateCommandHandler(IReviewRepository reviewRepository, IEventBus bus, IMapper map)
        {
            _reviewRepository = reviewRepository;
            _bus = bus;
            _map = map;
        }
        public async Task HandleAsync(ReviewCommand command)
        {
            try
            {
                var review = _map.Map<Review>(command);
                await _reviewRepository.UpsertReview(review);

                await _bus.PublishEvent(new ReviewCreatedEvent(command.RestaurantId,command.Id));
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception", $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}
