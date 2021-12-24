using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IUserService _userService;
        private readonly ILoggerService _loggerService;

        public BlogPostController(IBlogPostService blogPostService, IUserService userService, ILoggerService loggerService)
        {
            _blogPostService = blogPostService;
            _userService = userService;
            _loggerService = loggerService;
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
                bool inRange = Math.Abs(blogPostCount - (pg * cnt) - ((cnt * -1) + 1)) >= 1;

                (bool isIntAdminId, int adminUserId) = _userService.GetId(User);

                if ((page == null || !validPage) || (count == null || !validCount) || (blogPostCount > 0 && !inRange) || (userId != null && (!validIdInt || !(await _userService.IsValidId(id)))))
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }

                List<BlogPost> blogPosts;
                if (validIdInt && !isIntAdminId)
                {
                    blogPosts = await _blogPostService.Read(pg, cnt, id);
                }
                else if (isIntAdminId)
                {
                    blogPosts = await _blogPostService.AdminRead(pg, cnt, adminUserId);
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
                return _loggerService.Error(ex);
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
                return _loggerService.Error(ex);
            }
        }

        // POST api/<BlogPostController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BlogPost blogPost)
        {
            try
            {
                (bool idInfo, int id) = _userService.GetId(User);

                if (!idInfo)
                {
                    return new BadRequestResult();
                }

                BlogPost createdBlogPost = await _blogPostService.Create(blogPost, id);
                return new CreatedResult($"api/blogpost/{createdBlogPost.Id}", createdBlogPost);
            }
            catch (DbUpdateException ex)
            {
                return _loggerService.Info(ex);
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        // PATCH api/<BlogPostController>/5
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] BlogPost blogPost)
        {
            try
            {
                (bool isInt, int id) = _userService.GetId(User);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                BlogPost updatedBlogPost = await _blogPostService.Update(blogPost, id);

                if (updatedBlogPost == null)
                {
                    return new BadRequestResult();
                }

                return new OkObjectResult(new ResponseBody<BlogPost>(updatedBlogPost, typeof(BlogPost).Name.ToString(), null));
            }
            catch (DbUpdateException ex)
            {
                return _loggerService.Info(ex);
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        // DELETE api/<BlogPostController>/5
        [Authorize]
        [HttpDelete("{id}")]
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
            catch (DbUpdateException ex)
            {
                return _loggerService.Info(ex);
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }
    }
}
