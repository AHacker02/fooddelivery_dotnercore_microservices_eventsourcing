using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OMF.Common.Models;

namespace OMF.Common.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Generate Auth token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tokenSecret"></param>
        /// <returns></returns>
        public static string GenerateJwtToken(this User user, string tokenSecret)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static void Copy<T>(this T destination, T source)
        {
            var sourceProperties = source.GetType().GetProperties();
            var destProperties = destination.GetType().GetProperties();

            foreach (var src in sourceProperties)
            {
                foreach (var dest in destProperties)
                {
                    if (src.GetValue(source) != null && src.Name == dest.Name && src.PropertyType == dest.PropertyType )
                    {
                        dest.SetValue(destination, src.GetValue(source));
                        break;
                    }
                }
                
            }
        }
    }
}