using System;

namespace OMF.OrderManagementService.Command.Application.Commands
{
    public class CancelOrderCommand : ServiceBus.Abstractions.Command
    {
        public Guid UserId { get; set; }
    }

}
