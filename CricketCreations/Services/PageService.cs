using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreationsRepository.Models;
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
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<PageService, PageDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<PageService>> GetAll(int? id)
        {
            List<PageDTO> pageDTOs = await PageDTO.GetAll();
            List<PageService> pages = pageDTOs.ConvertAll(p => mapper.Map<PageDTO, PageService>(p));
            return pages;
        }
        public async Task<PageService> Create(PageService page, int userId)
        {
            PageDTO pageDTO = mapper.Map<PageService, PageDTO>(page);
            PageDTO newPageDTO = await PageDTO.Create(pageDTO);
            PageService newPage = mapper.Map<PageService>(newPageDTO);
            return newPage;
        }
        public async Task<PageService> Update(PageService page)
        {
            PageDTO pageDTO = mapper.Map<PageDTO>(page);
            PageDTO updatePageDTO = await PageDTO.Update(pageDTO);
            PageService updatedPage = mapper.Map<PageService>(updatePageDTO);
            return updatedPage;
        }

        public async Task<PageService> GetById(int id, bool? myBool)
        {
            PageDTO pageDTO = await PageDTO.GetById(id);
            return mapper.Map<PageService>(pageDTO);
        }

        public async Task<int> GetCount()
        {
            return await PageDTO.GetCount();
        }

        public async Task<List<PageService>> GetRange(int page, int count, int? id)
        {
            List<PageDTO> pageDTOs = await PageDTO.GetRange(page, count, id);
            return pageDTOs.ConvertAll(p => mapper.Map<PageDTO, PageService>(p));
        }

        public async Task<bool> Delete(int id)
        {
            return await PageDTO.Delete(id);
        }
    }
}
