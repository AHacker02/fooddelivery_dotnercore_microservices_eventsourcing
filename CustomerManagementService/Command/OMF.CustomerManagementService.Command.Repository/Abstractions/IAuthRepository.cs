using OMF.Common.Models;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Command.Repository.Abstractions
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<bool> UserExists(string email);
        Task DeleteUser(User userToDelete, string password);
    }
}
