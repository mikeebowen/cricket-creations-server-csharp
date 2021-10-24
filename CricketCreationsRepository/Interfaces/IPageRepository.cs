using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface IPageRepository : IRepository<PageDTO>
    {
        public abstract Task<PageDTO> Create(PageDTO pageDTO, int userId);
    }
}
