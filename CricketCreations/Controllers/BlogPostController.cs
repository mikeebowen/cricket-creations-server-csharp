using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class BlogPostController : ControllerBase
    {
        IBlogPostService _blogPostService;
        IUserService _userService;

        public BlogPostController(IBlogPostService blogPostService, IUserService userService)
        {
            _blogPostService = blogPostService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count, [FromQuery(Name = "userId")] string userId)
        {
            try
            {
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                bool validIdInt = int.TryParse(userId, out int id);
                int blogPostCount = userId != null && validIdInt ? await _blogPostService.GetCount(id) : await _blogPostService.GetCount();
                bool inRange = blogPostCount - (pg * cnt) >= ((cnt * -1) + 1);

                if ((page == null || !validPage) || (count == null || !validCount) || (blogPostCount > 0 && !inRange) || (userId != null && (!validIdInt || !(await _userService.IsValidId(id)))))
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }

                List<BlogPost> blogPosts;
                if (userId != null && page != null && count != null)
                {
                    blogPosts = await _blogPostService.Read(pg, cnt, id);
                }
                else
                {
                    blogPosts = await _blogPostService.Read(pg, cnt);
                }

                if (blogPosts.Count == 0)
                {
                    return new OkObjectResult(new ResponseBody<List<BlogPost>>(new List<BlogPost>(), typeof(BlogPost).Name.ToString(), blogPostCount));
                }

                return new OkObjectResult(new ResponseBody<List<BlogPost>>(blogPosts, typeof(BlogPost).Name.ToString(), blogPostCount));

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                BlogPost blogPost = await _blogPostService.Read(id);
                if (blogPost != null)
                {
                    return new OkObjectResult(new ResponseBody<BlogPost>(blogPost, typeof(BlogPost).Name.ToString(), null));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<BlogPostController>
        [Authorize, HttpPost("{userId}")]
        public async Task<IActionResult> Post([FromBody] BlogPost blogPost, int userId)
        {
            try
            {
                BlogPost createdBlogPost = await _blogPostService.Create(blogPost, userId);
                return new CreatedResult($"api/blogpost/{createdBlogPost.Id}", createdBlogPost);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
        }

        // PATCH api/<BlogPostController>/5
        [Authorize, HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] BlogPost blogPost)
        {
            try
            {
                BlogPost updatedBlogPost = await _blogPostService.Update(blogPost);
                return new OkObjectResult(new ResponseBody<BlogPost>(updatedBlogPost, typeof(BlogPost).Name.ToString(), null));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<BlogPostController>/5
        [Authorize, HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _blogPostService.Delete(id))
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
