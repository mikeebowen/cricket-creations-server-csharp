using CricketCreations.Interfaces;
using CricketCreations.Middleware;
using CricketCreations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class TagController : ControllerBase
    {
        ITagService _tagService;
        IUserService _userService;

        public TagController(ITagService tagService, IUserService userService)
        {
            _tagService = tagService;
            _userService = userService;
        }
        // GET: api/<TagController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count, [FromQuery(Name = "userId")] string userId)
        {
            try
            {
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                bool validIdInt = int.TryParse(userId, out int id);
                int blogPostCount = userId != null && validIdInt ? await _tagService.GetCount(id) : await _tagService.GetCount();
                bool inRange = blogPostCount - (pg * cnt) >= ((cnt * -1) + 1);

                if ((page == null || !validPage) || (count == null || !validCount) || (blogPostCount > 0 && !inRange) || (userId != null && (!validIdInt || !(await _userService.IsValidId(id)))))
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }

                List<Tag> tags;
                if (userId != null && page != null && count != null)
                {
                    tags = await _tagService.Read(pg, cnt, id);
                }
                else
                {
                    tags = await _tagService.Read(pg, cnt);
                }

                if (tags.Count == 0)
                {
                    return new OkObjectResult(new ResponseBody<List<Tag>>(new List<Tag>(), typeof(Tag).Name.ToString(), blogPostCount));
                }

                return new OkObjectResult(new ResponseBody<List<Tag>>(tags, typeof(Tag).Name.ToString(), blogPostCount));

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<TagController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagData data)
        {
            try
            {
                Tag newTag = await _tagService.Create(new Tag() { Name = data.Name }, data.BlogPostId, data.UserId);
                return new CreatedResult($"api/tag/{newTag.Id}", newTag);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<TagController>/5
        [Authorize, HttpPatch]
        public async Task<IActionResult> Patch([FromBody] Tag tag)
        {
            try
            {
                Tag updatedTag = await _tagService.Update(tag);
                return new OkObjectResult(new ResponseBody<Tag>(updatedTag, typeof(Tag).Name.ToString(), null));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<TagController>/5
        [Authorize, HttpDelete("{id}")]
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
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
