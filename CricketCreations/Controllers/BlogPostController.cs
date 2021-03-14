using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        // GET: api/<BlogPostController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<BlogPost>>>> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count, [FromQuery(Name = "userId")] string userId)
        {
            return await Controller<BlogPost>.Get(page, count, userId);
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Get(int id)
        {
            return await Controller<BlogPost>.GetById(id, null);
        }

        // POST api/<BlogPostController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Post([FromBody] JsonElement json)
        {
            return await Controller<BlogPost>.Post(json);
        }

        // PATCH api/<BlogPostController>/5
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Patch(int id, [FromBody] JsonElement json)
        {
            return await Controller<BlogPost>.Patch(id, json.ToString());
        }

        // DELETE api/<BlogPostController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Controller<BlogPost>.Delete(id);
        }
    }
}
