using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IUserService _userService;
        private readonly ILoggerService _loggerService;
        private readonly IBlogPostService _blogPostService;

        public TagController(ITagService tagService, IUserService userService, ILoggerService loggerService, IBlogPostService blogPostService)
        {
            _tagService = tagService;
            _userService = userService;
            _loggerService = loggerService;
            _blogPostService = blogPostService;
        }

        // GET: api/<TagController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count, [FromQuery(Name = "tagName")] string tagName)
        {
            try
            {
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                int blogPostCount = tagName != null ? await _tagService.GetCountOfBlogPosts(tagName) : await _tagService.GetCount();
                bool inRange = blogPostCount - (pg * cnt) >= ((cnt * -1) + 1);

                if ((page == null || !validPage) || (count == null || !validCount) || (blogPostCount > 0 && !inRange))
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }

                List<BlogPost> blogPosts;
                if (tagName != null && page != null && count != null)
                {
                    blogPosts = await _blogPostService.ReadByTagName(pg, cnt, tagName);
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
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

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Tag tag = await _tagService.Read(id);
                return new OkObjectResult(new ResponseBody<Tag>(tag, typeof(Tag).Name.ToString(), null));
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        // POST api/<TagController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagData data)
        {
            try
            {
                (bool isInt, int id) = _userService.GetId(User);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                Tag newTag = await _tagService.Create(new Tag() { Name = data.Name }, data.BlogPostId, id);
                return new CreatedResult($"api/tag/{newTag.Id}", newTag);
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

        // PUT api/<TagController>/5
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] Tag tag)
        {
            try
            {
                (bool isInt, int id) = _userService.GetId(User);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                Tag updatedTag = await _tagService.Update(tag, id);

                if (updatedTag == null)
                {
                    return new BadRequestResult();
                }

                return new OkObjectResult(new ResponseBody<Tag>(updatedTag, typeof(Tag).Name.ToString(), null));
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

        // DELETE api/<TagController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _tagService.Delete(id))
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

        public class TagData : IValidatableObject
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public int UserId { get; set; }

            [Required]
            public int BlogPostId { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                List<ValidationResult> validationResults = new List<ValidationResult>();
                if (UserId == 0)
                {
                    validationResults.Add(new ValidationResult("The UserId field is required"));
                }

                if (BlogPostId == 0)
                {
                    validationResults.Add(new ValidationResult("The BlogPostId field is required"));
                }

                return validationResults;
            }
        }
    }
}
