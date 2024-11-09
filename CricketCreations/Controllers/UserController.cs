using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoggerService _loggerService;

        public UserController(IUserService userService, ILoggerService loggerService)
        {
            _userService = userService;
            _loggerService = loggerService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return new OkObjectResult(await _userService.GetUser(id));
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
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
                return _loggerService.Error(ex);
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
                return _loggerService.Error(ex);
            }
        }

        // Uncomment this if you want to enable new user registration
        // POST api/<UserController>
        // [HttpPost]
        // public async Task<IActionResult> Post([FromBody] NewUser newUser)
        // {
        //    try
        //    {
        //        User createdUser = await _userService.Create(newUser);
        //        return new CreatedResult($"api/user/{createdUser.Id}", createdUser);
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return _loggerService.Info(ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return _loggerService.Error(ex);
        //    }
        // }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] NewUser user)
        {
            try
            {
                (bool isInt, int id) = _userService.GetId(User);

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
                return _loggerService.Info(ex);
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Authorize]
        [HttpPost("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] NewUser user)
        {
            (bool isInt, int id) = _userService.GetId(User);

            if (!isInt)
            {
                return new BadRequestResult();
            }

            bool updated = await _userService.UpdatePassword(id, user.Password);

            if (updated)
            {
                return new StatusCodeResult(StatusCodes.Status200OK);
            }

            return new BadRequestResult();
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                (bool isInt, int id) = _userService.GetId(User);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                await _userService.Logout(id);

                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }
            catch (DbUpdateException ex)
            {
                return _loggerService.Info(ex);
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        [HttpPost("set-reset-code")]
        public async Task<IActionResult> SetResetCode([FromBody] PasswordReset emailAddress)
        {
            try
            {
                bool codeSet = await _userService.SetResetPasswordCode(emailAddress.EmailAddress);

                if (codeSet)
                {
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        [HttpPost("validate-reset-code/{id}")]
        public async Task<IActionResult> ValidateResetCode(int id, [FromBody] PasswordReset passwordReset)
        {
            try
            {
                User user = await _userService.ValidateResetCode(id, passwordReset.ResetCode);

                if (user == null)
                {
                    return new UnauthorizedObjectResult("Login failed");
                }

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }
    }
}
