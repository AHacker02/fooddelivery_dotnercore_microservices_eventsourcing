using System;
using System.ComponentModel.DataAnnotations;

namespace OMF.CustomerManagementService.Command.Service.Command
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
        [RegularExpression(
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")]
        public string Email { get; set; }

        [Required] [MinLength(8)] public string Password { get; set; }
    }
}