using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        //IWebHostEnvironment env;
        //ImageController(IWebHostEnvironment hostingEnvironment)
        //{
        //    env = hostingEnvironment;
        //}
        // GET: api/<ImageController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ImageController>
        [HttpPost]
        public void Post([FromBody] string json)
        {
            Console.WriteLine(json);
            //dynamic fileData = JObject.Parse(json);
            //string filePath = System.IO.Path.Combine(env.WebRootPath, fileData.fileName);
            //byte[] fileBytes = Convert.FromBase64String(fileData.data);
            //System.IO.File.WriteAllBytes(filePath, fileData);
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
