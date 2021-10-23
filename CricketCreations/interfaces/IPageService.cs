using CricketCreations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IPageService : IApiService<Page>
    {
        public abstract Task<List<Page>> Read();
    }
}
