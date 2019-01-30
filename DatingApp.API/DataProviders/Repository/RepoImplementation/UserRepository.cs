using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DataProviders.Domain;
using DatingApp.API.DataProviders.Entities;
using DatingApp.API.DataProviders.Repository.GenericRepository;
using DatingApp.API.DataProviders.Repository.RepoContracts;
using DatingApp.API.Security;

namespace DatingApp.API.DataProviders.Repository.RepoImplementation {
    public class UserRepository : GenericRepository<User>, IUserRepository {
       
        public UserRepository (DatingAppContext context) : base (context) {
        }

        public User LogIn (string username, string password) {
            
            var user =  QueryAsync(u =>u.Username.ToLower().Equals(username)).Result.FirstOrDefault();

            if(user == null) 
                return null;
            
            if(!SecurityHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) 
                return null;
            
            return user;
        }

        

        public async Task<User> Register (User user, string password) {

            byte[] passwordHash, passwordSalt;

            user.Username = user.Username.ToLower();
            SecurityHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await AddAsync(user);
            await SaveAsync();

            return user;
        }

        

        public async Task<bool> UserExists (string username) {
            if (await ExistsAsync(x => x.Username == username))
                return true;
            return false;
        }
    }
}