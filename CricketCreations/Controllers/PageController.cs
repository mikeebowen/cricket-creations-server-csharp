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
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
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
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("include-unpublished")]
        [Authorize]
        public async Task<IActionResult> AdminGet()
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

                List<Page> pages = await _pageService.AdminRead(id);
                return new OkObjectResult(new ResponseBody<List<Page>>(pages, typeof(Page).Name.ToString(), pages.Count()));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<PageController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Page page)
        {
            try
            {
                List<Claim> claims = User.Claims.ToList();
                string idStr = claims?.FirstOrDefault(c => c.Type.Equals("Id", StringComparison.OrdinalIgnoreCase))?.Value;
                bool isInt = int.TryParse(idStr, out int userId);

                if (!isInt)
                {
                    return new BadRequestResult();
                }

                Page createdPage = await _pageService.Create(page, userId);
                return new CreatedResult($"api/page/{createdPage.Id}", createdPage);
            }
            catch (DbUpdateException ex)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<PageController>/5
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] Page page)
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

                Page updatedPage = await _pageService.Update(page, id);

                if (updatedPage == null)
                {
                    return new BadRequestResult();
                }

                return new OkObjectResult(new ResponseBody<Page>(updatedPage, typeof(Page).Name.ToString(), null));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
