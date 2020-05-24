using System;

namespace OMF.CustomerManagementService.Command.Service.Event
{
    public class UserCreatedEvent : ServiceBus.Abstractions.Event
    {
        public UserCreatedEvent(string email, Guid id) : base(id)
        {
            Email = email;
        }

        public string Email { get; }
    }
}