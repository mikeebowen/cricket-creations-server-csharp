﻿using CricketCreations.Models;
using CricketCreationsRepository.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IBlogPostService : IApiService<BlogPost>
    {
        public abstract Task<List<BlogPost>> AdminRead(int page, int count, int userId);
    }
}
