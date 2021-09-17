using CricketCreationsRepository.Models;
using CricketCreationsRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface IBlogPostRepository
    {
        public abstract Task<BlogPostDTO> Create(BlogPostDTO blogPostDTO, int userId);
        public abstract Task<List<BlogPostDTO>> Read();
        public abstract Task<List<BlogPostDTO>> Read(int page, int count);
        public abstract Task<List<BlogPostDTO>> Read(int page, int count, int id);
        public abstract Task<BlogPostDTO> Read(int id);
        public abstract Task<BlogPostDTO> Update(BlogPostDTO bloPostDTO);
        public abstract Task<bool> Delete(int id);
        public abstract Task<int> GetCount();
    }
}
