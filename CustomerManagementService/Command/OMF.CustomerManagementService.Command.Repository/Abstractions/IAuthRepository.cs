using System.Threading.Tasks;
using OMF.CustomerManagementService.Command.Repository.DataContext;

namespace OMF.CustomerManagementService.Command.Repository.Abstractions
{
    public interface IAuthRepository
    {
        /// <summary>
        /// Registers new user
        /// If user already exists then reactivates user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<TblCustomer> Register(TblCustomer user, string password);
        
        /// <summary>
        /// Check if user is present and active in system
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> UserExists(string email);
        
        /// <summary>
        /// Deactivate user
        /// </summary>
        /// <param name="userToDelete"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task DeleteUser(TblCustomer userToDelete, string password);

        
        /// <summary>
        /// Update user properties which are not null
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandPassword"></param>
        /// <returns></returns>
        Task UpdateUser(TblCustomer command, string commandPassword);
    }
}