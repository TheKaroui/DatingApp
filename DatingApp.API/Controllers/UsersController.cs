using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.DataProviders.Repository.RepoContracts;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UsersController (IUserRepository repo , IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers () {
            var users = await _userRepo.QueryAsync(u => true, u => u.Photos);

            var usersToReturn = _mapper.Map<IEnumerable<BasicUserCard>>(users);

            return Ok (usersToReturn);
        }

        [HttpGet ("{id}")]
        public IActionResult GetUser (int id) {

            var user = _userRepo.QueryAsync(u=> u.Id.Equals(id), u=>u.Photos).Result.SingleOrDefault();

            var userToReturn = _mapper.Map<FullUserCard>(user);

            return Ok (userToReturn);
        }
    }
}