using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class BlogPostController : ControllerBase
    {
        IApiService<BlogPost> _blogPostService;
        public BlogPostController(IApiService<BlogPost> blogPostService)
        {
            _blogPostService = blogPostService;
        }
        // GET: api/<BlogPostController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<BlogPost>>>> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count, [FromQuery(Name = "userId")] string userId)
        {
            return await _blogPostService.Get(page, count, userId);
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Get(int id)
        {
            return await _blogPostService.GetById(id);
        }

        // POST api/<BlogPostController>
        [Authorize, HttpPost("{userId}")]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Post([FromBody] JsonElement json, int userId)
        {
            return await _blogPostService.Post(json, userId);
        }

        // PATCH api/<BlogPostController>/5
        [Authorize, HttpPatch()]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Patch([FromBody] JsonElement json)
        {
            return await _blogPostService.Patch(json.ToString());
        }

        // DELETE api/<BlogPostController>/5
        [Authorize, HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _blogPostService.Delete(id);
        }
    }
}
