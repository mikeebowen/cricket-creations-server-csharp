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
        public Task<IEnumerable<CricketCreations.Models.User>> Get()
        {
            return CricketCreations.Models.User.GetAll();
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public Task<User> Get(int id)
        {
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
