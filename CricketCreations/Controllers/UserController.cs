﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CricketCreations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CricketCreationsRepository.Repositories;
using CricketCreations.Services;
using Microsoft.AspNetCore.Authorization;
using CricketCreations.Interfaces;
using CricketCreations.Models;

namespace CricketCreations.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class UserController : ControllerBase
    {
        IControllerService<UserService> _user;

        public UserController(IControllerService<UserService> user)
        {
            _user = user;
        }
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
        [Authorize, HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseBody<List<UserService>>>> Get() => await _user.Get(null, null, null);

        // GET: api/User/5
        [Authorize, HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ResponseBody<UserService>>> Get(int id, [FromQuery(Name = "withPosts")] string withPosts)
        {
            return await _user.GetById(id, withPosts == "true");
        }

        // POST: api/User
        [Authorize, HttpPost]
        public void Post([FromBody] JsonElement json)
        {
            Console.WriteLine("Hello World");
        }

        // PUT: api/User/5
        [Authorize, HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [Authorize, HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> CheckPassword([FromBody] JsonElement json)
        {
            PasswordObj vals = JsonConvert.DeserializeObject<PasswordObj>(json.ToString());
            UserService user = Services.UserService.CheckPassword(vals.Password, vals.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                string token = jwt.GenerateSecurityToken(user);
                string refreshToken = jwt.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiration = DateTime.Now.AddDays(7);
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
            UserService userInstance = new UserService();
            UserService user = await Services.UserService.CheckRefreshToken(refreshRequest.Id, refreshRequest.RefreshToken);
            if (user == null)
            {
                return BadRequest("Invalid Client Request");
            }
            string token = jwt.GenerateSecurityToken(user);
            string refreshToken = jwt.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddDays(7);
            await userInstance.Update(user);
            return new ObjectResult(new
            {
                token = token,
                refreshToken = refreshToken
            });
        }
    }
}
