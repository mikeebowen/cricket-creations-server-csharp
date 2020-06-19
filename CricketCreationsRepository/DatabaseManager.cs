using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CricketCreationsDatabase;

namespace CricketCreationsRepository
{
    public class DatabaseManager
    {
        static DatabaseManager()
        {
            Instance = new CricketCreationsContext();
        }
        public static CricketCreationsContext Instance { get; private set; }
    }
}
