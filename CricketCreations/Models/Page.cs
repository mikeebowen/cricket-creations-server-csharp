using AutoMapper;
using CricketCreations.interfaces;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class Page: IDataModel<Page>
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
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<Page, PageDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<Page>> GetAll(int? id)
        {
            List<PageDTO> pageDTOs = await PageDTO.GetAll();
            List<Page> pages = pageDTOs.ConvertAll(p => mapper.Map<PageDTO, Page>(p));
            return pages;
        }
        public async Task<Page> Create(Page page)
        {
            PageDTO pageDTO = mapper.Map<Page, PageDTO>(page);
            PageDTO newPageDTO = await PageDTO.Create(pageDTO);
            Page newPage = mapper.Map<Page>(newPageDTO);
            return newPage;
        }
        public async Task<Page> Update(Page page)
        {
            PageDTO pageDTO = mapper.Map<PageDTO>(page);
            PageDTO updatePageDTO = await PageDTO.Update(pageDTO);
            Page updatedPage = mapper.Map<Page>(updatePageDTO);
            return updatedPage;
        }

        public async Task<Page> GetById(int id, bool? myBool)
        {
            PageDTO pageDTO = await PageDTO.GetById(id);
            return mapper.Map<Page>(pageDTO);
        }

        public async Task<int> GetCount()
        {
            return await PageDTO.GetCount();
        }

        public async Task<List<Page>> GetRange(int page, int count, int? id)
        {
            List<PageDTO> pageDTOs = await PageDTO.GetRange(page, count, id);
            return pageDTOs.ConvertAll(p => mapper.Map<PageDTO, Page>(p));
        }

        public async Task<bool> Delete(int id)
        {
            return await PageDTO.Delete(id);
        }
    }
}
