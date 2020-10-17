using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using CricketCreationsDatabase.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async void Post([FromBody] JsonElement json)
        {
            //JObject jObject = JObject.Parse(value);
            // BlogPost blogPost = Newtonsoft.Json.JsonConvert.DeserializeObject<BlogPost>(json.ToString());
            //JsonSchemaGenerator jsonSchemaGenerator = new JsonSchemaGenerator();
            //JsonSchema jsonSchema = jsonSchemaGenerator.Generate(typeof(BlogPost));
            //JObject jObject = JObject.Parse(json.ToString());
            string jsonString = json.ToString();
            NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<BlogPost>();
            ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

            if (erros.Count == 0)
            {
                CricketCreations.Models.BlogPost blogPost = Newtonsoft.Json.JsonConvert.DeserializeObject<CricketCreations.Models.BlogPost>(jsonString);
                CricketCreations.Models.BlogPost post = await CricketCreations.Models.BlogPost.Create(blogPost);
                Ok();
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
    }
}
