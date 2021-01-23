using CricketCreations.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        // GET: api/<TagController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<Tag>>>> GetAll()
        {
            try
            {
                List<Tag> tags = await Tag.GetAll();
                ResponseBody<List<Tag>> response = new ResponseBody<List<Tag>>(tags, "Tag", tags.Count);
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TagController>
        [HttpPost]
        public async Task<ActionResult<Tag>> Post([FromBody] JsonElement json)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<TagPostBody>();
                ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

                if (erros.Count == 0)
                {
                    TagPostBody tagPostBody = Newtonsoft.Json.JsonConvert.DeserializeObject<TagPostBody>(jsonString);
                    BlogPost blogPost = await BlogPost.GetById(tagPostBody.BlogPostId);
                    List<BlogPost> blogPosts = new List<BlogPost>();
                    blogPosts.Add(blogPost);
                    Tag tag = new Tag { Name = tagPostBody.Name, BlogPosts = blogPosts };
                    Tag newTag = await Tag.Create(tag);
                    newTag.BlogPosts = blogPosts;
                    ResponseBody<Tag> response = new ResponseBody<Tag>(newTag, "Tag", null);
                    return Created($"api/tag/{tag.Id}", response);
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

        // PUT api/<TagController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TagController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private class TagPostBody
        {
            public string Name { get; set; }
            public int BlogPostId { get; set; }
        }
    }
}
