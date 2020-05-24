using OMF.Common.Models;

namespace OMF.CustomerManagementService.Query.Repository.Models
{
    public class UserAuthToken
    {
        public string Token { get; set; }
        public User User { get; set; }

    }
}
