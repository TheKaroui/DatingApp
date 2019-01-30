using System;
using System.Threading.Tasks;
using DatingApp.API.DataProviders.Repository.RepoContracts;
using DatingApp.API.Models;
using DatingApp.API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public AuthController (IUserRepository userRepository, IConfiguration config) {
            this._config = config;
            this._userRepository = userRepository;
        }

        //We don't refer [FromBody because we use [ApiController]
        // public async Task<IActionResult> Register ([FromBody] UserModel usrModel)
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register (UserToRegisterModel usrModel) 
        {
            //TODO Validate request
            if (await _userRepository.UserExists (usrModel.UserName.ToLower ()))
                return BadRequest ("User name already exists");

            var userToCreate = new DataProviders.Entities.User {
                Username = usrModel.UserName.ToLower()
            };

            var createdUser = await _userRepository.Register (userToCreate, usrModel.Password);
            // return CreatedAtRoute()
            return StatusCode (201);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserToLoginModel userToLogin)
        {
            var userFromDb = _userRepository.LogIn(userToLogin.UserName.ToLower(), userToLogin.Password);
            if (userFromDb == null) return Unauthorized();

            var result = SecurityHelper.GetSecurityToken(userFromDb, _config.GetSection("AppSettings:Token").Value);

            return Ok(result);
        }


    }
}