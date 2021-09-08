using CricketCreations.Interfaces;
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
    public class PageController : ControllerBase
    {
        IControllerService<Page> _page;

        public PageController(IControllerService<Page> page)
        {
            _page = page;
        }
        // GET: api/<PageController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<Page>>>> Get()
        {
            return await _page.Get(null, null, null);
        }

        // GET api/<PageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBody<Page>>> Get(int id)
        {
            return await _page.GetById(id, null);
        }

        // POST api/<PageController>
        [Authorize, HttpPost("{userId}")]
        public async Task<ActionResult<ResponseBody<Page>>> Post([FromBody] JsonElement json, int userId)
        {
            return await _page.Post(json, userId);
        }

        // PUT api/<PageController>/5
        [Authorize, HttpPatch()]
        public async Task<ActionResult<ResponseBody<Page>>> Patch([FromBody] JsonElement json, int userId)
        {
            return await _page.Patch(json.ToString());
        }

        // DELETE api/<PageController>/5
        [Authorize, HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _page.Delete(id);
        }
    }
}
