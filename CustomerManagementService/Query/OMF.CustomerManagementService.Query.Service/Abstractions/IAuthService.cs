using OMF.CustomerManagementService.Query.Application.Models;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Query.Application.Services
{
    public interface IAuthService
    {
        Task<UserAuthToken> Login(string email, string password);
    }
}
