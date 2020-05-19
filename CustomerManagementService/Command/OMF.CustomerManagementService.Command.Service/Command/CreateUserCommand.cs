using System.ComponentModel.DataAnnotations;

namespace OMF.CustomerManagementService.Command.Service.Command
{
    public class CreateUserCommand : ServiceBus.Abstractions.Command
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")]
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
