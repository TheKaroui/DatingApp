using System.Collections.Generic;
using DatingApp.API.DataProviders.Entities;
using DatingApp.API.DataProviders.Repository.RepoContracts;
using DatingApp.API.Security;
using Newtonsoft.Json;

namespace DatingApp.API.DataProviders.Domain.SeedData
{
    public class Seed
    {
        private readonly IUserRepository _uRepo;
        public Seed(IUserRepository ur)
        {
            _uRepo = ur;
        }

        public void SeedUsers() 
        {
            var userData = System.IO.File.ReadAllText("DataProviders/Domain/SeedData/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                SecurityHelper.CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();

                _uRepo.Add(user);
            }

            _uRepo.Save();
        }

        
    }
}