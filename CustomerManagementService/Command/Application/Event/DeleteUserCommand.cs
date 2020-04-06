using System;
using System.ComponentModel.DataAnnotations;

namespace OMF.CustomerManagementService.Command.Application.Event
{
    public class DeleteUserCommand : ServiceBus.Abstractions.Command
    {
        public DeleteUserCommand()
        {

        }
        public DeleteUserCommand(string email, string password, Guid id) : base(id)
        {
            Email = email;
            Password = password;
        }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

    }
}
