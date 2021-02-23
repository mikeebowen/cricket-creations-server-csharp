﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CricketCreations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CricketCreationsRepository.Models;
using CricketCreations.Services;

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration config;
        private JwtService jwt;
        private class PasswordObj
        {
            public string Password { get; set; }
            public string Email { get; set; }
        }
        private class RefreshRequest
        {
            public int Id { get; set; }
            public string RefreshToken { get; set; }
        }
        public UserController(IConfiguration configuration)
        {
            config = configuration;
            jwt = new JwtService(config);
        }
        // GET: api/User
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseBody<List<User>>>> Get() => await Controller<User>.Get(null, null, null);

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ResponseBody<User>>> Get(int id, [FromQuery(Name = "withPosts")] string withPosts)
        {
            return await Controller<User>.GetById(id, withPosts == "true");
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] JsonElement json)
        {
            Console.WriteLine("Hello World");
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> CheckPassword([FromBody] JsonElement json)
        {
            PasswordObj vals = JsonConvert.DeserializeObject<PasswordObj>(json.ToString());
            User user = Models.User.CheckPassword(vals.Password, vals.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                string token = jwt.GenerateSecurityToken(user);
                string refreshToken = jwt.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(60);
                await user.Update(user);
                return new ObjectResult(new
                {
                    token = token,
                    refreshToken = refreshToken
                });
            }
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] JsonElement json)
        {
            RefreshRequest refreshRequest = JsonConvert.DeserializeObject<RefreshRequest>(json.ToString());
            User userInstance = new User();
            User user = await userInstance.GetById(refreshRequest.Id, null);
            if (user == null || user.RefreshToken != refreshRequest.RefreshToken)
            {
                return BadRequest("Invalid Client Request");
            }
            string token = jwt.GenerateSecurityToken(user);
            string refreshToken = jwt.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            await userInstance.Update(user);
            return new ObjectResult(new {
                token = token,
                refreshToken = refreshToken
            });
        }
    }
}
