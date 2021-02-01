using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
        [HttpPost]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Post([FromBody] JsonElement json)
        {
            return await Controller<BlogPost>.Post(json);
        }

        // PATCH api/<BlogPostController>/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Patch(int id, [FromBody] JsonElement json)
        {
            return await Controller<BlogPost>.Patch(id, json);
        }

        // DELETE api/<BlogPostController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Controller<BlogPost>.Delete(id);
        }
    }
}
