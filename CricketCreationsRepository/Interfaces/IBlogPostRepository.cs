using System.Collections.Generic;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;

namespace CricketCreationsRepository.Interfaces
{
    public interface IBlogPostRepository : IRepository<BlogPostDTO>
    {
        public abstract Task<BlogPostDTO> Create(BlogPostDTO blogPostDTO, int userId);

        public abstract Task<List<BlogPostDTO>> AdminRead(int page, int count, int id);

        public abstract Task<List<BlogPostDTO>> ReadByTagName(int page, int count, string tagName);
    }
}
