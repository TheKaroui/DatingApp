using System.Threading.Tasks;
using DatingApp.API.DataProviders.Repository.RepoContracts;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUserRepository _userRepo;
        //private readonly IMapper _mapper;
        public UsersController (IUserRepository repo) //, IMapper mapper)
        {
            //_mapper = mapper;
            _userRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers () {
            var users = await _userRepo.QueryAsync(u => true, u => u.Photos);

            //var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok (users);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetUser (int id) {
            var user = await _userRepo.GetByIdAsync(id);

            //var userToReturn = _mapper.Map<UserForDetailedDto> (user);

            return Ok (user);
        }
    }
}