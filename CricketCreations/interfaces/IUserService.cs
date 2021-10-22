using CricketCreations.Models;
using CricketCreations.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IUserService
    {
        public abstract Task<User> GetUser(int id);
    }
}
