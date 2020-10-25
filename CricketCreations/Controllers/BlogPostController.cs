using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreations.Models;
using CricketCreationsDatabase.Models;
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
        public async Task<IActionResult> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count)
        {
            try
            {
                string response;
                if (int.TryParse(page, out int pg) && int.TryParse(count, out int cnt))
                {

                    List<CricketCreations.Models.BlogPost> blogPosts = await CricketCreations.Models.BlogPost.GetRange(pg, cnt);
                    response = new ResponseBody<List<Models.BlogPost>>(blogPosts, "BlogPosts").GetJson();
                }
                else
                {
                    List<CricketCreations.Models.BlogPost> blogPosts = await CricketCreations.Models.BlogPost.GetAll();
                    response = new ResponseBody<List<Models.BlogPost>>(blogPosts, "BlogPosts").GetJson();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                CricketCreations.Models.BlogPost blogPost = await CricketCreations.Models.BlogPost.GetById(id);
                if (blogPost != null)
                {
                    string response = new ResponseBody<CricketCreations.Models.BlogPost>(blogPost, "BlogPost").GetJson();
                    return Ok(response);
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
        public async Task<IActionResult> Post([FromBody] JsonElement json)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<Models.BlogPost>();
                ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

                if (erros.Count == 0)
                {
                    Models.BlogPost blogPost = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.BlogPost>(jsonString);
                    blogPost.Created = DateTime.Now;
                    blogPost.LastUpdated = blogPost.Created ?? DateTime.Now;
                    Models.BlogPost post = await Models.BlogPost.Create(blogPost);
                    string response = new ResponseBody<Models.BlogPost>(post, "BlogPost").GetJson();
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
        public async Task<IActionResult> Patch(int id, [FromBody] JsonElement json)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<Models.BlogPost>();
                ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

                if (erros.Count == 0)
                {
                    Models.BlogPost blogPost = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.BlogPost>(jsonString);
                    blogPost.LastUpdated = DateTime.Now;
                    blogPost.Id = id;
                    Models.BlogPost post = await Models.BlogPost.Update(blogPost);
                    if (post != null)
                    {
                        string response = new ResponseBody<Models.BlogPost>(post, "BlogPost").GetJson();
                        return Ok(response);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleted = await CricketCreations.Models.BlogPost.Delete(id);
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
        private class ErrorObject
        {
            public string Message { get; set; }
            public string Property { get; set; }
        }
    }
}
