using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Security {
    public static class SecurityHelper {
        public static void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) 
                    return false;
                }
            }
            return true;
        }

        //TODO resolve config service to get token value from appsettings.json directly
        public static object GetSecurityToken (DataProviders.Entities.User userFromDb, string tokenValue) {
            var claims = new [] {
                new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromDb.Username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValue));

            //key hashing (encryption)
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new {
                token = tokenHandler.WriteToken(token)
            };
        }

    }
}