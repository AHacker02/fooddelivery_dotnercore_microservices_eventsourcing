using System.Threading.Tasks;
using OMF.CustomerManagementService.Query.Repository.Models;

namespace OMF.CustomerManagementService.Query.Service.Abstractions
{
    public interface IAuthService
    {
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserAuthToken> Login(string email, string password);
    }
}
