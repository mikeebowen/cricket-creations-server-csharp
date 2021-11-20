using CricketCreationsDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface IDatabaseManager
    {
        public CricketCreationsContext Instance { get; set; }
    }
}
