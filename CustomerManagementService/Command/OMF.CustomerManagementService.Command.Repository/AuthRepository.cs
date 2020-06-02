using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMF.Common.Helpers;
using OMF.CustomerManagementService.Command.Repository.Abstractions;
using OMF.CustomerManagementService.Command.Repository.DataContext;

namespace OMF.CustomerManagementService.Command.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CustomerManagementContext _database;

        public AuthRepository(CustomerManagementContext database)
        {
            _database = database;
        }

        public async Task<TblCustomer> Register(TblCustomer user, string password)
        {
            var dbuser = _database.TblCustomer.FirstOrDefault(x => x.Email.Equals(user.Email));
            if (dbuser != null) return await ReactivateUser(dbuser, password);

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.Password = passwordHash;
            user.PasswordKey = passwordSalt;
            user.CreatedDate = DateTime.UtcNow;
            user.Active = true;
            await _database.TblCustomer.AddAsync(user);
            return await _database.SaveChangesAsync() > 0 ? user : null;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _database.TblCustomer.AnyAsync(x => x.Email.Equals(email) && x.Active);
        }

        public async Task DeleteUser(TblCustomer userToDelete, string password)
        {
            var user = _database.TblCustomer.FirstOrDefault(x => x.Email.Equals(userToDelete.Email));
            if (!VerifyPasswordHash(password, user.Password, user.PasswordKey))
                throw new InvalidOperationException("The password entered is incorrect");

            user.Active = false;
            await _database.SaveChangesAsync();
        }

        public async Task UpdateUser(TblCustomer updatedUser, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
                updatedUser.Password = passwordHash;
                updatedUser.PasswordKey = passwordSalt;
            }

            var user = _database.TblCustomer.FirstOrDefault(x => x.Email.Equals(updatedUser.Email));
            updatedUser.CreatedDate = user.CreatedDate;
            updatedUser.Id = user.Id;
            user.Copy(updatedUser);
            await _database.SaveChangesAsync();
        }

        private async Task<TblCustomer> ReactivateUser(TblCustomer user, string password)
        {
            if (!VerifyPasswordHash(password, user.Password, user.PasswordKey))
                throw new InvalidOperationException("The password entered is incorrect");

            user.Active = true;
            user.ModifiedDate = DateTime.UtcNow;
            return await _database.SaveChangesAsync() > 0 ? user : null;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (var i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != passwordHash[i])
                        return false;
            }

            return true;
        }
    }
}