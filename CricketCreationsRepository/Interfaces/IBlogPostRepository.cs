using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface IBlogPostRepository
    {
        public abstract Task<List<BlogPostDTO>> GetAll(int? id);
        public abstract Task<int> GetCount();
        public abstract Task<List<BlogPostDTO>> GetRange(int page, int count, int? id);
    }
}
