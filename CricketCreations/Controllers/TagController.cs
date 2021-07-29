using CricketCreations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class TagController : ControllerBase
    {
        // GET: api/<TagController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<Tag>>>> GetAll()
        {
            return await Controller<Tag>.Get(null, null, null);
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TagController>
        [Authorize, HttpPost]
        public async Task<ActionResult<ResponseBody<Tag>>> Post([FromBody] JsonElement json)
        {
            return await Controller<Tag>.Post(json);
        }

        // PUT api/<TagController>/5
        [Authorize, HttpPatch("{id}")]
        public void Patch(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TagController>/5
        [Authorize, HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private class TagPostBody
        {
            public string Name { get; set; }
            public int BlogPostId { get; set; }
        }
    }
}
