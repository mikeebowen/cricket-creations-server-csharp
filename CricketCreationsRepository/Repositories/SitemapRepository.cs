using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;

namespace CricketCreationsRepository.Repositories
{
    public class SitemapRepository : ISitemapRepository
    {
        private IDatabaseManager _databaseManager;

        public SitemapRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public List<SitemapUrlDTO> GetSitemapUrls()
        {
            List<Page> pageDTOs = _databaseManager.Instance.Page.Where(p => p.Published == true && p.Deleted != true).ToList();
            List<BlogPost> blogPostDTOs = _databaseManager.Instance.BlogPost.Where(b => b.Published == true && b.Deleted != true).ToList();

            List<SitemapUrlDTO> sitemapUrlDTOs = new List<SitemapUrlDTO>();

            pageDTOs.ForEach(p =>
            {
                sitemapUrlDTOs.Add(new SitemapUrlDTO() { Created = p.Created.Value.ToString("yyyy-MM-dd"), Location = string.Concat("/", HttpUtility.UrlPathEncode(p.Heading)) });
            });

            blogPostDTOs.ForEach(b =>
            {
                // add id and replace all whitespace with dashes
                string loc = string.Concat(b.Id.ToString(), '-', b.Title.Replace(" ", "-"));
                loc = Regex.Replace(loc, "[^a-zA-Z0-9-_]", string.Empty);
                byte[] tmpBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(loc);
                string location = string.Concat("/blog/", System.Text.Encoding.UTF8.GetString(tmpBytes).ToLower());
                sitemapUrlDTOs.Add(new SitemapUrlDTO() { Created = b.Created.Value.ToString("yyyy-MM-dd"), Location = location });
            });

            return sitemapUrlDTOs;
        }
    }
}
