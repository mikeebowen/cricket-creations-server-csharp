using System.Collections.Generic;
using System.Threading.Tasks;
using CricketCreations.Models;

namespace CricketCreations.Interfaces
{
    public interface IBlogPostService : IApiService<BlogPost>
    {
        public abstract Task<List<BlogPost>> AdminRead(int page, int count, int userId);

        public abstract Task<List<BlogPost>> ReadByTagName(int page, int count, string tagName);
    }
}
