using System;
using System.Threading.Tasks;
using DatingApp.API.DataProviders.Entities;
using DatingApp.API.DataProviders.Repository.RepoContracts;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingAPP.API.Controllers {

    /// <summary>
    /// by default the kestrel web server lestens on port 5000 => http://localhost:5000/api/values
    /// </summary>
    // [Authorize] no need after we added the filter globally in startup.cs
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        private readonly IValueRepository _valueRepository;

        //REST API not RESTFUL, it will be more pragmatic, so using http verbs
        // GET api/values

        // injected context (automatically recognized injecton?)
        public ValuesController (IValueRepository ValueRepository) {
            this._valueRepository = ValueRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetValues () {
            return Ok (await _valueRepository.GetAllAsync ());
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetValueById (int id) {
            return Ok (await _valueRepository.GetByIdAsync (id));
        }

        // POST api/values
        [HttpPost ("add")]
        public async Task<IActionResult> Post ([FromBody] ValueToAddModel vtam) {
            var valueToAdd = new Value { Name = vtam.Name };
            await _valueRepository.AddAsync(valueToAdd);
            await _valueRepository.SaveAsync();
            return StatusCode(201);
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}