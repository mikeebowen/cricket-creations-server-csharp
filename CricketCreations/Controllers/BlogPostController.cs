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
        IControllerService<BlogPostService> _blogPost;
        public BlogPostController(IControllerService<BlogPostService> blogPost)
        {
            _blogPost = blogPost;
        }
        // GET: api/<BlogPostController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<BlogPostService>>>> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count, [FromQuery(Name = "userId")] string userId)
        {
            return await _blogPost.Get(page, count, userId);
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBody<BlogPostService>>> Get(int id)
        {
            return await _blogPost.GetById(id, null);
        }

        // POST api/<BlogPostController>
        [Authorize, HttpPost("{userId}")]
        public async Task<ActionResult<ResponseBody<BlogPostService>>> Post([FromBody] JsonElement json, int userId)
        {
            return await _blogPost.Post(json, userId);
        }

        // PATCH api/<BlogPostController>/5
        [Authorize, HttpPatch()]
        public async Task<ActionResult<ResponseBody<BlogPostService>>> Patch([FromBody] JsonElement json)
        {
            return await _blogPost.Patch(json.ToString());
        }

        // DELETE api/<BlogPostController>/5
        [Authorize, HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _blogPost.Delete(id);
        }
    }
}
