using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;

namespace CricketCreationsRepository.Interfaces
{
    public interface ISitemapRepository
    {
        public abstract List<SitemapUrlDTO> GetSitemapUrls();
    }
}
