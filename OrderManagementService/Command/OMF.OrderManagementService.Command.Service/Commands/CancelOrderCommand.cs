using System;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class CancelOrderCommand : ServiceBus.Abstractions.Command
    {
        public Guid UserId { get; set; }
    }

}
