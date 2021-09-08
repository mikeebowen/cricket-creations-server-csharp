using CricketCreations.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IControllerService<T>
    {
        public abstract Task<ActionResult<ResponseBody<T>>> GetById(int id, bool? include);
        public abstract Task<ActionResult<ResponseBody<List<T>>>> Get(string page, string count, string userId);
        public abstract Task<ActionResult<ResponseBody<T>>> Post(JsonElement json, int userId);
        public abstract Task<ActionResult<ResponseBody<T>>> Patch(string jsonString);
        public abstract Task<ActionResult> Delete(int id);
    }
}
