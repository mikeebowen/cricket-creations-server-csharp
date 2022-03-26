using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CricketCreations.interfaces;
using CricketCreations.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace CricketCreations.Controllers
{
    public class SitemapController : Controller
    {
        private ISitemapService _sitemapService;

        public SitemapController(ISitemapService sitemapService)
        {
            _sitemapService = sitemapService;
        }

        [Route("/sitemap.xml")]
        public void Index()
        {
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();

            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }

            string host = Request.Scheme + "://" + Request.Host;

            Response.ContentType = "application/xml";

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host);
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", string.Concat(host, "/projects"));
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", string.Concat(host, "/blog"));
                xml.WriteEndElement();


                List<SitemapUrl> sitemapUrls = _sitemapService.GetSiteMap();

                sitemapUrls.ForEach(s =>
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", string.Concat(host, s.Location));
                    xml.WriteElementString("lastmod", s.LastModified);
                    xml.WriteEndElement();
                });

                xml.WriteEndElement();
            }
        }
    }
}
