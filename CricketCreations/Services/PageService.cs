using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreationsRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Services
{
    public class PageService : IDataService<PageService>
    {
        private int? id;
        public int? Id { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public int? UserId
        {
            get
            {
                return id;
            }
            set
            {
                if (value > 0)
                {
                    id = value;
                }
            }
        }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<PageService, PageRepository>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<PageService>> GetAll(int? id)
        {
            List<PageRepository> pageDTOs = await PageRepository.GetAll();
            List<PageService> pages = pageDTOs.ConvertAll(p => mapper.Map<PageRepository, PageService>(p));
            return pages;
        }
        public async Task<PageService> Create(PageService page, int userId)
        {
            PageRepository pageDTO = mapper.Map<PageService, PageRepository>(page);
            PageRepository newPageDTO = await PageRepository.Create(pageDTO);
            PageService newPage = mapper.Map<PageService>(newPageDTO);
            return newPage;
        }
        public async Task<PageService> Update(PageService page)
        {
            PageRepository pageDTO = mapper.Map<PageRepository>(page);
            PageRepository updatePageDTO = await PageRepository.Update(pageDTO);
            PageService updatedPage = mapper.Map<PageService>(updatePageDTO);
            return updatedPage;
        }

        public async Task<PageService> GetById(int id, bool? myBool)
        {
            PageRepository pageDTO = await PageRepository.GetById(id);
            return mapper.Map<PageService>(pageDTO);
        }

        public async Task<int> GetCount()
        {
            return await PageRepository.GetCount();
        }

        public async Task<List<PageService>> GetRange(int page, int count, int? id)
        {
            List<PageRepository> pageDTOs = await PageRepository.GetRange(page, count, id);
            return pageDTOs.ConvertAll(p => mapper.Map<PageRepository, PageService>(p));
        }

        public async Task<bool> Delete(int id)
        {
            return await PageRepository.Delete(id);
        }
    }
}
