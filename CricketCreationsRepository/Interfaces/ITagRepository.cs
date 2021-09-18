using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface ITagRepository : IRepository<TagDTO>
    {
        public abstract Task<TagDTO> Create(TagDTO tagDTO, int blogPostId, int userId);
    }
}
