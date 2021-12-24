using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CricketCreations.Interfaces
{
    public interface ILoggerService
    {
        public abstract ObjectResult Info(DbUpdateException ex);

        public abstract StatusCodeResult Error(Exception ex);
    }
}
