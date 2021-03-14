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
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        // GET: api/<PageController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<Page>>>> Get()
        {
            return await Controller<Page>.Get(null, null, null);
        }

        // GET api/<PageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBody<Page>>> Get(int id)
        {
            return await Controller<Page>.GetById(id, null);
        }

        // POST api/<PageController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseBody<Page>>> Post([FromBody] JsonElement json)
        {
            return await Controller<Page>.Post(json);
        }

        // PUT api/<PageController>/5
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseBody<Page>>> Patch(int id, [FromBody] JsonElement json)
        {
            return await Controller<Page>.Patch(id, json.ToString());
        }

        // DELETE api/<PageController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Controller<Page>.Delete(id);
        }
    }
}
