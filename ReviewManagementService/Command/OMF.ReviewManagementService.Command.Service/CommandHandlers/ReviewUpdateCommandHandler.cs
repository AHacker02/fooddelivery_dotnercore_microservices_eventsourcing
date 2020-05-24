using System;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Events;
using OMF.ReviewManagementService.Command.Repository.Abstractions;
using OMF.ReviewManagementService.Command.Repository.DataContext;
using OMF.ReviewManagementService.Command.Service.Command;
using ServiceBus.Abstractions;

namespace OMF.ReviewManagementService.Command.Service.CommandHandlers
{
    public class ReviewUpdateCommandHandler : ICommandHandler<ReviewCommand>
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

        public async Task HandleAsync(ReviewCommand command)
        {
            try
            {
                var review = _map.Map<TblRating>(command);
                await _reviewRepository.UpsertReview(review);
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}