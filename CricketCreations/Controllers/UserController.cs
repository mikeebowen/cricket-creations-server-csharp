using CricketCreations.Interfaces;
using CricketCreations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return new OkObjectResult(await _userService.GetUser(id));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] CheckPasswordRequest checkPasswordRequest)
        {
            try
            {
                AuthenticationResponse res = await _userService.CheckPassword(checkPasswordRequest.UserName, checkPasswordRequest.Password);

                if (res == null)
                {
                    return new UnauthorizedObjectResult("Login failed");
                }

                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] CheckRefreshTokenRequest request)
        {
            try
            {
                AuthenticationResponse res = await _userService.CheckRefreshToken(request.Id, request.RefreshToken);

                if (res == null)
                {
                    return new UnauthorizedObjectResult("Token authentication failed");
                }
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewUser newUser)
        {
            try
            {
                User createdUser = await _userService.Create(newUser);
                return new CreatedResult($"api/tag/{createdUser.Id}", createdUser);
            }
            catch (DbUpdateException ex)
            {
                return new ObjectResult(new { Errors = new[] { new { Message = ex.Message } } }) { StatusCode = StatusCodes.Status303SeeOther };
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
