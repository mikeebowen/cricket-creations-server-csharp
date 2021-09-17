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
        public abstract Task<List<BlogPostRepository>> GetAll(int? id);
        public abstract Task<int> GetCount();
        public abstract Task<List<BlogPostRepository>> GetRange(int page, int count, int? id);
        public abstract Task<BlogPostRepository> GeyById(int id);
    }
}
