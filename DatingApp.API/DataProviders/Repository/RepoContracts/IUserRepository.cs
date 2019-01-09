using System.Threading.Tasks;
using DatingApp.API.DataProviders.Entities;
using DatingApp.API.DataProviders.Repository.GenericRepository;

namespace DatingApp.API.DataProviders.Repository.RepoContracts {
    public interface IUserRepository : IGenericRepository<User> {
        Task<User> Register (User user, string password);
        User LogIn (string username, string password);
        Task<bool> UserExists (string username);

    }
}