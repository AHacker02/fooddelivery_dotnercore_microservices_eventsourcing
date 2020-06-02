using System.Threading.Tasks;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.EventHandlers
{
    public class ItemPriceUpdateEventHandler : IEventHandler<ItemPriceUpdateEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public ItemPriceUpdateEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(ItemPriceUpdateEvent @event)
        {
            await _orderRepository.UpdatePrice(@event);
        }
    }
}