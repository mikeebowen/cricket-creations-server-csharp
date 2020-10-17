using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreationsDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
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
        public async Task<IActionResult> GetResultAsync()
        {
            List<CricketCreations.Models.BlogPost> blogPosts = await CricketCreations.Models.BlogPost.GetAll();
            return Ok(blogPosts);
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlogPostController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonElement json)
        {
            string jsonString = json.ToString();
            NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<BlogPost>();
            ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

            if (erros.Count == 0)
            {
                CricketCreations.Models.BlogPost blogPost = Newtonsoft.Json.JsonConvert.DeserializeObject<CricketCreations.Models.BlogPost>(jsonString);
                blogPost.Created = DateTime.Now;
                blogPost.LastUpdated = blogPost.Created;
                CricketCreations.Models.BlogPost post = await CricketCreations.Models.BlogPost.Create(blogPost);
                return Created($"api/blogpost/{post.Id}", post);
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

        // PUT api/<BlogPostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogPostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private class ErrorObject
        {
            public string Message { get; set; }
            public string Property { get; set; }
        }
    }
}
