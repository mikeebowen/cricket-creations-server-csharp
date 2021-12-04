using CricketCreations.Interfaces;
using CricketCreations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Authorize, HttpGet("{id}")]
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
                User user = await _userService.CheckPassword(checkPasswordRequest.UserName, checkPasswordRequest.Password);

                if (user == null)
                {
                    return new UnauthorizedObjectResult("Login failed");
                }

                return new OkObjectResult(user);
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
                return new CreatedResult($"api/user/{createdUser.Id}", createdUser);
            }
            catch (DbUpdateException ex)
            {
                return new ObjectResult(new { Errors = new[] { new { Message = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message } } }) { StatusCode = StatusCodes.Status303SeeOther };
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<UserController>/5
        [Authorize, HttpPatch]
        public async Task<IActionResult> Patch([FromBody] NewUser user)
        {
            try
            {
                List<Claim> claims = User.Claims.ToList();
                string idStr = claims?.FirstOrDefault(c => c.Type.Equals("Id", StringComparison.OrdinalIgnoreCase))?.Value;
                bool isInt = int.TryParse(idStr, out int id);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                User newUser = await _userService.Update(user, id);
                if (newUser != null)
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            catch (DbUpdateException ex)
            {
                return new ObjectResult(new { Errors = new[] { new { Message = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message } } }) { StatusCode = StatusCodes.Status303SeeOther };
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
