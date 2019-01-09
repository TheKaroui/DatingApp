using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DataProviders.Domain;
using DatingApp.API.DataProviders.Entities;
using DatingApp.API.DataProviders.Repository.GenericRepository;
using DatingApp.API.DataProviders.Repository.RepoContracts;

namespace DatingApp.API.DataProviders.Repository.RepoImplementation {
    public class UserRepository : GenericRepository<User>, IUserRepository {
       
        public UserRepository (DatingAppContext context) : base (context) {
        }

        public User LogIn (string username, string password) {
            
            var user =  QueryAsync(u =>u.UserName.Equals(username)).Result.FirstOrDefault();

            if(user == null) 
                return null;
            
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) 
                return null;
            
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) return false;
                }
            }
            return false;
        }

        public async Task<User> Register (User user, string password) {

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await AddAsync(user);
            await SaveAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists (string username) {
            if (await ExistsAsync(x => x.UserName == username))
                return true;
            return false;
        }
    }
}