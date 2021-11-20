using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CricketCreationsDatabase;
using CricketCreationsRepository.Interfaces;

namespace CricketCreationsRepository
{
    public class DatabaseManager : IDatabaseManager
    {
        public DatabaseManager()
        {
            Instance = new CricketCreationsContext();
        }
        public CricketCreationsContext Instance { get; set; }
    }
}
