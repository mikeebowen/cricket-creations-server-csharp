using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using CricketCreations.interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;

namespace CricketCreations.Services
{
    public class SitemapService : ISitemapService
    {
        private ISitemapRepository _sitemapRepository;
        private IMapper _mapper;

        public SitemapService(ISitemapRepository sitemapRepository, IMapper mapper)
        {
            _sitemapRepository = sitemapRepository;
            _mapper = mapper;
        }

        public List<SitemapUrl> GetSiteMap()
        {
            return _sitemapRepository.GetSitemapUrls().Select(s => {
                return new SitemapUrl() { Location = s.Location, LastModified = s.LastModified };
            }).ToList();
        }
    }
}
