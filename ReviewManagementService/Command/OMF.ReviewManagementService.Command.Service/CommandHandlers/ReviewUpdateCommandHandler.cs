using System;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.ReviewManagementService.Command.Repository.Abstractions;
using OMF.ReviewManagementService.Command.Service.Command;
using OMF.ReviewManagementService.Command.Service.Event;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Command.Service.CommandHandlers
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
