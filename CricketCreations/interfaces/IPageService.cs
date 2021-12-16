using System.Collections.Generic;
using System.Threading.Tasks;
using CricketCreations.Models;

namespace CricketCreations.Interfaces
{
    public interface IPageService : IApiService<Page>
    {
        public abstract Task<List<Page>> Read();
    }
}
