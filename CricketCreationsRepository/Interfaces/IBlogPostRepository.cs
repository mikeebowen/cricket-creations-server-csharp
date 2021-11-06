using CricketCreationsRepository.Models;
using CricketCreationsRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface IBlogPostRepository : IRepository<BlogPostDTO>
    {
        public abstract Task<BlogPostDTO> Create(BlogPostDTO blogPostDTO, int userId);
        public abstract Task<List<BlogPostDTO>> AdminRead(int page, int count, int id);
    }
}
