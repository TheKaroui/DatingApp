using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DataProviders.Domain;
using DatingApp.API.DataProviders.Repository.RepoContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAPP.API.Controllers {
    /// <summary>
    /// by default the kestrel web server lestens on port 5000 => http://localhost:5000/api/values
    /// </summary>
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        private readonly IValueRepository _ValueRepository;

        //REST API not RESTFUL, it will be more pragmatic, so using http verbs
        // GET api/values

        // injected context (automatically recognized injecton?)
        public ValuesController (IValueRepository ValueRepository) {
            this._ValueRepository = ValueRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetValues () {
            return Ok (await _ValueRepository.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetValueById (int id) {
            return Ok (await _ValueRepository.GetByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}