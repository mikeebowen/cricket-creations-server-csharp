using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CricketCreations.Interfaces
{
    public interface IImageService
    {
        public abstract Task<Dictionary<string, string>> Save(IFormFile formFile, string host);
    }
}
