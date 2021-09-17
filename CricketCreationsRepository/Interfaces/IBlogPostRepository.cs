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
        public abstract Task<BlogPostRepository> Create(BlogPostRepository blogPostDTO, int userId);
        public abstract Task<List<BlogPostRepository>> Read();
        public abstract Task<List<BlogPostRepository>> Read(int page, int count);
        public abstract Task<List<BlogPostRepository>> Read(int page, int count, int id);
        public abstract Task<BlogPostRepository> Read(int id);
        public abstract Task<BlogPostRepository> Update(BlogPostRepository bloPostDTO);
        public abstract Task<int> GetCount();
    }
}
