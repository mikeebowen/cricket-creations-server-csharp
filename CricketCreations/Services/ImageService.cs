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
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            string saveFolderPath;
            string saveFolderName;

            if (directoryInfos.Length == 0)
            {
                saveFolderPath = Path.Join(directoryPath, "Images", "0");
                Directory.CreateDirectory(saveFolderPath);
                saveFolderName = "0";
            }
            else
            {
                saveFolderName = (directoryInfos.Length - 1).ToString();
                saveFolderPath = Path.Join(directoryPath, "Images", saveFolderName);
                Directory.CreateDirectory(saveFolderPath);
                DirectoryInfo saveFolderInfo = new DirectoryInfo(saveFolderPath);
                FileInfo[] files = saveFolderInfo.GetFiles();

                if (files.Length > 99)
                {
                    saveFolderName = directoryInfos.Length.ToString();
                    saveFolderPath = Path.Join(directoryPath, "Images", saveFolderName);
                    Directory.CreateDirectory(saveFolderPath);
                }
            }

            string filePath = Path.Combine(saveFolderPath, fName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var dict = new Dictionary<string, string>();
            dict.Add("location", $"//{host}/Images/{saveFolderName}/{fName}");

            return dict;
        }
    }
}
