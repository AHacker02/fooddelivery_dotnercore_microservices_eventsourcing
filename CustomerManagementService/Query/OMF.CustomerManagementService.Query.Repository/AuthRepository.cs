using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Models;
using OMF.CustomerManagementService.Query.Repository.Abstractions;
using OMF.CustomerManagementService.Query.Repository.DataContext;

namespace OMF.CustomerManagementService.Query.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private CustomerManagementContext _database;
        private readonly IMapper _map;

        public AuthRepository(CustomerManagementContext database,IMapper map)
        {
            _database = database;
            _map = map;
        }
        public async Task<User> Login(string email, string password)
        {
            var user = _map.Map<User>(_database.TblCustomer.FirstOrDefault(x=>x.Email.Equals(email)));
            if (user == null)
            {
                return null;
            }
            return !VerifyPasswordHash(password, user.Password, user.PasswordKey) ? null : user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

    }
}
