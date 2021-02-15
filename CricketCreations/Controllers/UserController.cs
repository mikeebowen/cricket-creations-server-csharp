using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private class PasswordObj
        {
            public string Password { get; set; }
            public string Email { get; set; }
        }
        // GET: api/User
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseBody<List<User>>>> Get() => await Controller<User>.Get(null, null, null);

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ResponseBody<User>>> Get(int id, [FromQuery(Name = "withPosts")] string withPosts)
        {
            return await Controller<User>.GetById(id, withPosts == "true");
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] JsonElement json)
        {
            Console.WriteLine("Hello World");
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost("password")]
        public IActionResult CheckPassword([FromBody] JsonElement json)
        {
            PasswordObj vals = JsonConvert.DeserializeObject<PasswordObj>(json.ToString());
            var response = Models.User.CheckPassword(vals.Password, vals.Email);
            if (response == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
