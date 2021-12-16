using System.Threading.Tasks;
using CricketCreationsRepository.Models;

namespace CricketCreationsRepository.Interfaces
{
    public interface ITagRepository : IRepository<TagDTO>
    {
        public abstract Task<TagDTO> Create(TagDTO tagDTO, int blogPostId, int userId);
    }
}
