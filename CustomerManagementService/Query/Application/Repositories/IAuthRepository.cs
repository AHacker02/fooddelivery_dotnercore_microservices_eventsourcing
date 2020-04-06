using OMF.Common.Models;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Query.Application.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Login(string email, string password);
    }
}
