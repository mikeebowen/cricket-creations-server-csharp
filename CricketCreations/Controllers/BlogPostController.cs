using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        // GET: api/<BlogPostController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<CricketCreations.Models.BlogPost> blogPosts = await CricketCreations.Models.BlogPost.GetAll();
            return Ok(blogPosts);
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlogPostController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            JObject jObject = JObject.Parse(value);
            Ok();
        }

        // PUT api/<BlogPostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogPostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
