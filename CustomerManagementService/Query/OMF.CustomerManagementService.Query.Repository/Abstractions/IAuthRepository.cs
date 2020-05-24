using System.Threading.Tasks;
using OMF.Common.Models;
using OMF.CustomerManagementService.Query.Repository.DataContext;

namespace OMF.CustomerManagementService.Query.Repository.Abstractions
{
    public interface IAuthRepository
    {
        /// <summary>
        /// Checks username and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Login(string email, string password);
    }
}
