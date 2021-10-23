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
    public interface ITagService : IApiService<Tag>
    {
        public abstract Task<Tag> Create(Tag tag, int blogPostId, int userId);
    }
}
