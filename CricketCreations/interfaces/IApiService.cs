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
        public abstract Task<ActionResult<ResponseBody<T>>> Create(JsonElement json, int userId);
        public abstract Task<ActionResult<ResponseBody<BlogPost>>> Read(int id);
        public abstract Task<ActionResult<ResponseBody<List<T>>>> Read(string page, string count);
        public abstract Task<ActionResult<ResponseBody<List<T>>>> Read(string page, string count, string userId);
        public abstract Task<ActionResult<ResponseBody<T>>> Update(string jsonString);
        public abstract Task<ActionResult> Delete(int id);
    }
}
