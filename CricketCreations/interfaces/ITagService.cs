using CricketCreations.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface ITagService : IApiService<Tag>
    {
        public abstract Task<ActionResult<ResponseBody<Tag>>> Create(string json, int blogPostId, int userId);
    }
}
