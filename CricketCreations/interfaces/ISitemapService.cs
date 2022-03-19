using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreations.Models;

namespace CricketCreations.interfaces
{
    public interface ISitemapService
    {
        public abstract List<SitemapUrl> GetSiteMap();
    }
}
