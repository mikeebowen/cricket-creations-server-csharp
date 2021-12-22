using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CricketCreations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CricketCreations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        // POST api/<ImageController>
        [HttpPost]
        public async Task<Dictionary<string, string>> UploadFile([FromForm] IFormFile file, [FromHeader] string host)
        {
            Dictionary<string, string> dict = await _imageService.Save(file, host);
            return dict;
        }
    }
}
