using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CricketCreations.Models;

namespace CricketCreations.Data
{
    public class CricketCreationsContext : DbContext
    {
        public CricketCreationsContext (DbContextOptions<CricketCreationsContext> options)
            : base(options)
        {
        }

        public DbSet<CricketCreations.Models.User> User { get; set; }

        public DbSet<CricketCreations.Models.BlogPost> BlogPost { get; set; }
    }
}
