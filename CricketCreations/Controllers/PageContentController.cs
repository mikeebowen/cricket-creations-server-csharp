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
    public class PageContentController : ControllerBase
    {
        // GET: api/<PageContentController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<PageContent>>>> Get()
        {
            List<PageContent> pageContents = await PageContent.GetAll();
            ResponseBody<List<PageContent>> response = new ResponseBody<List<PageContent>>(pageContents, "BlogPosts", pageContents.Count);
            return response;
        }

        // GET api/<PageContentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PageContentController>
        [HttpPost]
        public async Task<ActionResult<ResponseBody<PageContent>>> Post([FromBody] JsonElement json)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<PageContent>();
                ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

                if (erros.Count == 0)
                {
                    PageContent pageContent = Newtonsoft.Json.JsonConvert.DeserializeObject<PageContent>(jsonString);
                    pageContent.Created = DateTime.Now;
                    pageContent.LastUpdated = pageContent.Created;
                    PageContent content = await PageContent.Create(pageContent);
                    ResponseBody<PageContent> response = new ResponseBody<PageContent>(content, "PageContent", null);
                    return Created($"api/blogpost/{content.Id}", response);
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

        // PUT api/<PageContentController>/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseBody<PageContent>>> Patch(int id, [FromBody] JsonElement json)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<PageContent>();
                ICollection<NJsonSchema.Validation.ValidationError> erros = jsonSchema.Validate(jsonString);

                if (erros.Count == 0)
                {
                    PageContent pageContent = Newtonsoft.Json.JsonConvert.DeserializeObject<PageContent>(jsonString);
                    pageContent.LastUpdated = DateTime.Now;
                    pageContent.Id = id;
                    PageContent content = await PageContent.Update(pageContent);
                    if (content != null)
                    {
                        ResponseBody<PageContent> response = new ResponseBody<PageContent>(content, "BlogPost", null);
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

        // DELETE api/<PageContentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
