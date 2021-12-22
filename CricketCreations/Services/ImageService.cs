using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CricketCreations.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CricketCreations.Services
{
    public class ImageService : IImageService
    {
        public async Task<Dictionary<string, string>> Save(IFormFile file, string host)
        {
            Guid guid = Guid.NewGuid();
            string fName = $"{guid}-{file.FileName.Replace(" ", "_")}";
            string directoryPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot");

            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, fName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var dict = new Dictionary<string, string>();
            dict.Add("location", $"//{host}/{fName}");

            return dict;
        }
    }
}
