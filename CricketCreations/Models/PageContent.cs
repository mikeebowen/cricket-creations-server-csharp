using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;

namespace CricketCreations.Models
{
    public class PageContent
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<PageContent, PageContentDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<PageContent>> GetAll()
        {
            List<PageContentDTO> pageContentDTOs = await PageContentDTO.GetAll();
            List<PageContent> pageContents = pageContentDTOs.ConvertAll(p => mapper.Map<PageContent>(p));
            return pageContents;
        }
        public static async Task<PageContent> Create(PageContent pageContent)
        {
            PageContentDTO pageContentDTO = mapper.Map<PageContentDTO>(pageContent);
            PageContentDTO newPageContentDTO = await PageContentDTO.Create(pageContentDTO);
            PageContent newPageContent = mapper.Map<PageContent>(newPageContentDTO);
            return newPageContent;
        }
        public static async Task<PageContent> Update(PageContent pageContent)
        {
            PageContentDTO pageContentDTO = mapper.Map<PageContentDTO>(pageContent);
            PageContentDTO updatePageContentDTO = await PageContentDTO.Update(pageContentDTO);
            PageContent updatedPageContent = mapper.Map<PageContent>(updatePageContentDTO);
            return updatedPageContent;
        }
    }
}
