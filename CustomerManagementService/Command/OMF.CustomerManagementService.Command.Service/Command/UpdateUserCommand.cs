using System.ComponentModel.DataAnnotations;
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Service.Command
{
    public class UpdateUserCommand:ServiceBus.Abstractions.Command
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public string MobileNumber { get; set; }
        [MinLength(8)]
        public string Password { get; set; }

        public string Address { get; set; }
    }
}