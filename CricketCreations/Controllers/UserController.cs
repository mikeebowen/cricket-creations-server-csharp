using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Task<IEnumerable<User>>>> Get() => await Models.User.GetAll();

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public Task<User> Get(int id, [FromQuery(Name = "withPosts")] string withPosts)
        {
            if (withPosts == "true")
            {
                return Models.User.GetUserWithPosts(id);
            }
            return CricketCreations.Models.User.GetUser(id);
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
    }
}
