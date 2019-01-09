using DatingApp.API.DataProviders.Domain;
using DatingApp.API.DataProviders.Entities;
using DatingApp.API.DataProviders.Repository.GenericRepository;
using DatingApp.API.DataProviders.Repository.RepoContracts;

namespace DatingApp.API.DataProviders.Repository.RepoImplementation {
    public class ValueRepository : GenericRepository<Value>, IValueRepository {
        private readonly DatingAppContext _context;
        public ValueRepository (DatingAppContext context) : base (context) {
            this._context = context;
        }
    }
}