using System.Threading.Tasks;
using OMF.Common.Events;
using OMF.OrderManagementService.Command.Repository.Abstractions;
using ServiceBus.Abstractions;

namespace OMF.OrderManagementService.Command.Service.EventHandlers
{
    public class ItemOutOfStockEventHandler : IEventHandler<ItemOutOfStockEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public ItemOutOfStockEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(ItemOutOfStockEvent @event)
        {
            await _orderRepository.OrderOutOfStock(@event);
        }
    }
}