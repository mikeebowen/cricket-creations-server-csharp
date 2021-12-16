using System.Threading.Tasks;
using CricketCreations.Models;

namespace CricketCreations.Interfaces
{
    public interface ITagService : IApiService<Tag>
    {
        public abstract Task<Tag> Create(Tag tag, int blogPostId, int userId);
    }
}
