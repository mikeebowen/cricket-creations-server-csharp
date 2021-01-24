using CricketCreations.Models;
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
            return await DataHandler<Page>.Get(null, null, null);
        }

        // GET api/<PageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBody<Page>>> Get(int id)
        {
            return await DataHandler<Page>.GetById(id, null);
        }

        // POST api/<PageController>
        [HttpPost]
        public async Task<ActionResult<ResponseBody<Page>>> Post([FromBody] JsonElement json)
        {
            return await DataHandler<Page>.Post(json);
        }

        // PUT api/<PageController>/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseBody<Page>>> Patch(int id, [FromBody] JsonElement json)
        {
            return await DataHandler<Page>.Patch(id, json);
        }

        // DELETE api/<PageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
