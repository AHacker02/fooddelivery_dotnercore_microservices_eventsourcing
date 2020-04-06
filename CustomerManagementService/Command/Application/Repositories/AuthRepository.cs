using DataAccess.Abstractions;
using OMF.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Command.Application.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private INoSqlDataAccess _database;
        public AuthRepository(INoSqlDataAccess database)
        {
            _database = database;
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _database.Add<User>(user);

            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            return (await _database.All<User>()).Any(x => x.Email.Equals(email));
        }

        public async Task DeleteUser(User userToDelete, string password)
        {
            var user = (await _database.All<User>()).FirstOrDefault(x => x.Email.Equals(userToDelete.Email));
            if (!VerifyPasswordHash(password, user.Password, user.PasswordSalt))
            {
                throw new InvalidOperationException("The paswrod entered is incorrect");
            }

            await _database.Delete<User>(x => x.Email == userToDelete.Email);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
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
