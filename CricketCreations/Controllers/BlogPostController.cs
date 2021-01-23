using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        // GET: api/<BlogPostController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<BlogPost>>>> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count, [FromQuery(Name = "userId")] string userId)
        {
            try
            {
                ResponseBody<List<BlogPost>> response;
                List<BlogPost> blogPosts;
                bool validId = int.TryParse(userId, out int id);
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                bool inRange = await BlogPost.GetCount() - (pg * cnt) > ((cnt * -1) + 1);

                if (!inRange)
                {
                    return StatusCode(416);
                }

                if (validPage && validCount)
                {
                    if (validId)
                    {
                        blogPosts = await BlogPost.GetRange(pg, cnt, id);
                    }
                    else
                    {
                        blogPosts = await BlogPost.GetRange(pg, cnt, null);
                    }
                }
                else
                {
                    if (validId)
                    {
                        blogPosts = await BlogPost.GetAll(id);
                    }
                    else
                    {
                        blogPosts = await BlogPost.GetAll(null);
                    }

                }
                int blogPostCount = await BlogPost.GetCount();
                response = new ResponseBody<List<BlogPost>>(blogPosts, "BlogPosts", blogPostCount);
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Get(int id)
        {
            try
            {
                BlogPost blogPost = await BlogPost.GetById(id);
                if (blogPost != null)
                {
                    ResponseBody<BlogPost> response = new ResponseBody<BlogPost>(blogPost, "BlogPost", null);
                    return response;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/<BlogPostController>
        [HttpPost]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Post([FromBody] JsonElement json)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<BlogPost>();
                ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

                if (erros.Count == 0)
                {
                    BlogPost blogPost = Newtonsoft.Json.JsonConvert.DeserializeObject<BlogPost>(jsonString);
                    blogPost.Created = DateTime.Now;
                    blogPost.LastUpdated = blogPost.Created ?? DateTime.Now;
                    BlogPost post = await BlogPost.Create(blogPost);
                    ResponseBody<BlogPost> response = new ResponseBody<BlogPost>(post, "BlogPost", null);
                    return Created($"api/blogpost/{post.Id}", response);
                }
                else
                {
                    List<ErrorObject> errs = new List<ErrorObject>();
                    erros.ToList().ForEach(e =>
                    {
                        errs.Add(new ErrorObject() { Message = e.Kind.ToString(), Property = e.Property });
                    });
                    return BadRequest(errs);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PATCH api/<BlogPostController>/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseBody<BlogPost>>> Patch(int id, [FromBody] JsonElement json)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<BlogPost>();
                ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

                if (erros.Count == 0)
                {
                    BlogPost blogPost = Newtonsoft.Json.JsonConvert.DeserializeObject<BlogPost>(jsonString);
                    blogPost.LastUpdated = DateTime.Now;
                    blogPost.Id = id;
                    BlogPost post = await BlogPost.Update(blogPost);
                    if (post != null)
                    {
                        ResponseBody<BlogPost> response = new ResponseBody<BlogPost>(post, "BlogPost", null);
                        return response;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    List<ErrorObject> errs = new List<ErrorObject>();
                    erros.ToList().ForEach(e =>
                    {
                        errs.Add(new ErrorObject() { Message = e.Kind.ToString(), Property = e.Property });
                    });
                    return BadRequest(errs);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<BlogPostController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool deleted = await BlogPost.Delete(id);
                if (deleted)
                {
                    return StatusCode(204);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
