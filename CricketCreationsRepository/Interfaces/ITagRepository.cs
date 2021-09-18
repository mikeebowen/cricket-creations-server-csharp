using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface ITagRepository
    {
        public abstract Task<TagDTO> Create(TagDTO tagDTO, int blogPostId, int userId);
        public abstract Task<List<TagDTO>> Read();
        public abstract Task<TagDTO> Read(int tagId);
        public abstract Task<List<TagDTO>> Read(int page, int count);
        public abstract Task<int> GetCount();
    }
}
