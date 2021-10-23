using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
using CricketCreationsRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Services
{
    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;

        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        
        private static MapperConfiguration _config = new MapperConfiguration(c => c.CreateMap<PageService, PageRepository>().ReverseMap());
        private static IMapper _mapper = _config.CreateMapper();

        public async Task<List<Page>> Read()
        {
            List<PageDTO> pageDTOs = await _pageRepository.Read();
            return pageDTOs.Select(p => _convertToPage(p)).ToList();
        }
        public async Task<Page> Read(int id)
        {
            PageDTO pageDTO = await _pageRepository.Read(id);
            return _convertToPage(pageDTO);
        }

        public Task<List<Page>> Read(int page, int count)
        {
            throw new NotImplementedException();
        }

        public Task<List<Page>> Read(int page, int count, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Page> Update(Page page)
        {
            PageDTO pageDTO = _convertToPageDTO(page);
            PageDTO updatedPageDTO = await _pageRepository.Update(pageDTO);
            return _convertToPage(pageDTO);
        }

        public Task<Page> Create(Page t, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            return await _pageRepository.Delete(id);
        }

        public async Task<int> GetCount()
        {
            return await _pageRepository.GetCount();
        }

        public Task<int> GetCount(int id)
        {
            throw new NotImplementedException();
        }

        private PageDTO _convertToPageDTO(Page page)
        {
            if (page == null)
            {
                return null;
            }
            return _mapper.Map<PageDTO>(page);
        }

        private Page _convertToPage(PageDTO pageDTO)
        {
            if (pageDTO == null)
            {
                return null;
            }
            return _mapper.Map<Page>(pageDTO);
        }
    }
}
