using System.Collections.Generic;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;

namespace CricketCreationsRepository.Interfaces
{
    public interface IPageRepository : IRepository<PageDTO>
    {
        public abstract Task<PageDTO> Create(PageDTO pageDTO, int userId);

        public abstract bool IsUniquePageHeading(string pageHeading);

        public abstract bool IsUniquePageHeading(string pageHeading, int id);

        public abstract Task<List<PageDTO>> AdminRead(int id);
    }
}
