using System.ComponentModel.DataAnnotations;

namespace OMF.CustomerManagementService.Command.Service.Command
{
    public class CreateUserCommand : ServiceBus.Abstractions.Command
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
