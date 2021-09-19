using CricketCreations.Models;
using CricketCreationsRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IApiService<T>
    {
        public abstract Task<IActionResult> Read(int id);
        public abstract Task<IActionResult> Read(string page, string count);
        public abstract Task<IActionResult> Read(string page, string count, string userId);
        public abstract Task<IActionResult> Update(string jsonString);
        public abstract Task<IActionResult> Delete(int id);
    }
}
