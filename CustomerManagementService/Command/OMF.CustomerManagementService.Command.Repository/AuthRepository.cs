using OMF.Common.Models;
using OMF.CustomerManagementService.Command.Repository.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMF.CustomerManagementService.Command.Repository.DataContext;

namespace OMF.CustomerManagementService.Command.Repository
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
        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.Password = passwordHash;
            user.PasswordKey = passwordSalt;

            await _database.TblCustomer.AddAsync(_map.Map<TblCustomer>(user));
            return await _database.SaveChangesAsync() > 0 ? user : null;
        }

        public async Task<bool> UserExists(string email)
            =>  await _database.TblCustomer.AnyAsync(x => x.Email.Equals(email));
        

        public async Task DeleteUser(User userToDelete, string password)
        {
            var user = await _database.TblCustomer.FirstOrDefaultAsync(x => x.Email.Equals(userToDelete.Email));
            if (!VerifyPasswordHash(password, user.Password, user.PasswordKey))
            {
                throw new InvalidOperationException("The password entered is incorrect");
            }

            _database.Remove(user);
            await _database.SaveChangesAsync();
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
