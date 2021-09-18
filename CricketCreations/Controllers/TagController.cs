﻿using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class TagController : ControllerBase
    {
        ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        // GET: api/<TagController>
        [HttpGet]
        public async Task<ActionResult<ResponseBody<List<Tag>>>> Get([FromQuery(Name = "page")] string page, [FromQuery(Name = "count")] string count)
        {
            return await _tagService.Read(page, count);
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBody<Tag>>> Get(int id)
        {
            return await _tagService.Read(id);
        }

        // POST api/<TagController>
        [Authorize, HttpPost]
        public async Task<ActionResult<ResponseBody<Tag>>> Post([FromBody] TagData data)
        {
            return await _tagService.Create(JsonConvert.SerializeObject(new { Name = data.Name }), data.BlogPostId, data.UserId);
        }

        // PUT api/<TagController>/5
        [Authorize, HttpPatch]
        public async Task<ActionResult<ResponseBody<Tag>>> Patch([FromBody] JsonDocument json)
        {
            return await _tagService.Update(json.RootElement.ToString());
        }

        // DELETE api/<TagController>/5
        [Authorize, HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        public class TagData
        {
            public string Name { get; set; }
            public int UserId { get; set; }
            public int BlogPostId { get; set; }
        }
    }
}
