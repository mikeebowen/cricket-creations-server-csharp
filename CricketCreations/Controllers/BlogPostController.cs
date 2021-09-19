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
        IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count, [FromQuery(Name = "userId")] string userId)
        {
            return userId == null ? await _blogPostService.Read(page, count) : await _blogPostService.Read(page, count, userId);
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _blogPostService.Read(id);
        }

        // POST api/<BlogPostController>
        [Authorize, HttpPost("{userId}")]
        public async Task<IActionResult> Post([FromBody] JsonElement json, int userId)
        {
            return await _blogPostService.Create(json, userId);
        }

        // PATCH api/<BlogPostController>/5
        [Authorize, HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] JsonElement json)
        {
            return await _blogPostService.Update(json.ToString());
        }

        // DELETE api/<BlogPostController>/5
        [Authorize, HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _blogPostService.Delete(id);
        }
    }
}
