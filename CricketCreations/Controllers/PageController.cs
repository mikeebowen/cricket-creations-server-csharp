﻿using System;
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
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;
        private readonly IUserService _userService;
        private readonly ILoggerService _loggerService;

        public PageController(IPageService pageService, IUserService userService, ILoggerService loggerService)
        {
            _pageService = pageService;
            _userService = userService;
            _loggerService = loggerService;
        }

        // GET: api/<PageController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Page> pages = await _pageService.Read();
                return new OkObjectResult(new ResponseBody<List<Page>>(pages, typeof(Page).Name.ToString(), pages.Count()));
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        // GET api/<PageController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Page page = await _pageService.Read(id);
                return new OkObjectResult(new ResponseBody<Page>(page, typeof(Page).Name.ToString(), await _pageService.GetCount()));
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        [Authorize]
        [HttpGet("include-unpublished")]
        public async Task<IActionResult> AdminGet()
        {
            try
            {
                (bool isInt, int id) = _userService.GetId(User);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                List<Page> pages = await _pageService.AdminRead(id);
                return new OkObjectResult(new ResponseBody<List<Page>>(pages, typeof(Page).Name.ToString(), pages.Count()));
            }
            catch (Exception ex)
            {
                return _loggerService.Error(ex);
            }
        }

        // POST api/<PageController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Page page)
        {
            try
            {
                (bool isInt, int id) = _userService.GetId(User);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                Page createdPage = await _pageService.Create(page, id);
                return new CreatedResult($"api/page/{createdPage.Id}", createdPage);
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

        // PUT api/<PageController>/5
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] Page page)
        {
            try
            {
                (bool isInt, int id) = _userService.GetId(User);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                Page updatedPage = await _pageService.Update(page, id);

                if (updatedPage == null)
                {
                    return new BadRequestResult();
                }

                return new OkObjectResult(new ResponseBody<Page>(updatedPage, typeof(Page).Name.ToString(), null));
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

        // DELETE api/<PageController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _pageService.Delete(id))
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }
                else
                {
                    return new NotFoundResult();
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
    }
}
