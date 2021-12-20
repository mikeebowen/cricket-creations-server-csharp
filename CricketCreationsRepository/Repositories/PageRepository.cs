using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CricketCreationsRepository.Repositories
{
    public class PageRepository : IPageRepository
    {
        private static readonly MapperConfiguration _config = new MapperConfiguration(c => c.CreateMap<Page, PageDTO>().ReverseMap());
        private static readonly IMapper _mapper = _config.CreateMapper();

        private readonly IDatabaseManager _databaseManager;

        public PageRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<bool> Delete(int id)
        {
            Page page = await _databaseManager.Instance.Page.FindAsync(id);
            if (page != null)
            {
                page.Deleted = true;
                await _databaseManager.Instance.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> GetCount()
        {
            return await _databaseManager.Instance.Page.Where(p => p.Deleted == false).CountAsync();
        }

        public Task<int> GetCount(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PageDTO>> Read()
        {
            List<Page> pages = await _databaseManager.Instance.Page.Where(p => p.Deleted == false).ToListAsync();
            List<PageDTO> pageDTOs = pages.Select(p => _convertToPageDTO(p)).ToList();
            return pageDTOs;
        }

        public async Task<List<PageDTO>> Read(int page, int count)
        {
            List<Page> pages = await _databaseManager.Instance.Page.Where(p => p.Published == true).Skip((page - 1) * count).Take(count).ToListAsync();
            return pages.Select(p => _convertToPageDTO(p)).ToList();
        }

        public async Task<List<PageDTO>> Read(int page, int count, int id)
        {
            List<Page> pages = await _databaseManager.Instance.Page.Where(p => p.User.Id == id && p.Published).Skip((page - 1) * count).Take(count).ToListAsync();
            return pages.Select(p => _convertToPageDTO(p)).ToList();
        }

        public async Task<PageDTO> Read(int id)
        {
            Page page = await _databaseManager.Instance.Page.Where(p => p.User.Id == id && p.Published).FirstOrDefaultAsync();
            return _convertToPageDTO(page);
        }

        public async Task<List<PageDTO>> AdminRead(int id)
        {
            List<Page> pages = await _databaseManager.Instance.Page.Where(p => p.User.Id == id).ToListAsync();
            return pages.Select(p => _convertToPageDTO(p)).ToList();
        }

        public async Task<PageDTO> Update(PageDTO pageDTO, int userId)
        {
            User user = await _databaseManager.Instance.User.FindAsync(userId);
            if (user != null && user.Role == Role.Administrator)
            {
                Page page = await _databaseManager.Instance.Page.FindAsync(pageDTO.Id);

                if (page != null)
                {
                    Page updatedPage = _convertToPage(pageDTO);
                    _databaseManager.Instance.Entry(page).CurrentValues.SetValues(updatedPage);
                    PropertyEntry propertyEntry = _databaseManager.Instance.Entry(page).Property("Created");

                    if (propertyEntry != null)
                    {
                        _databaseManager.Instance.Entry(page).Property("Created").IsModified = false;
                    }

                    await _databaseManager.Instance.SaveChangesAsync();
                    return _convertToPageDTO(page);
                }
            }

            return null;
        }

        public async Task<PageDTO> Create(PageDTO pageDTO, int userId)
        {
            Page page = _convertToPage(pageDTO);
            User user = await _databaseManager.Instance.User.FindAsync(userId);
            user.Pages = user.Pages ?? new List<Page>();
            Page existingPage = await _databaseManager.Instance.Page.Where(p => p.Heading == page.Heading).FirstOrDefaultAsync();

            if (existingPage == null || existingPage.Deleted == false)
            {
                user.Pages.Add(page);
                page.User = user;

                _databaseManager.Instance.Page.Add(page);
                await _databaseManager.Instance.SaveChangesAsync();
                return _convertToPageDTO(page);
            }
            else
            {
                existingPage.Deleted = false;
                await _databaseManager.Instance.SaveChangesAsync();
                return _convertToPageDTO(existingPage);
            }
        }

        public bool IsUniquePageHeading(string pageHeading)
        {
            Page page = _databaseManager.Instance.Page.Where(p => p.Heading == pageHeading).FirstOrDefault();

            if (page == null || page.Deleted == true)
            {
                return true;
            }

            return false;
        }

        public bool IsUniquePageHeading(string pageHeading, int id)
        {
            Page page = _databaseManager.Instance.Page.Where(p => p.Heading == pageHeading).FirstOrDefault();

            if (page == null || page.Id == id || page.Deleted == true)
            {
                return true;
            }

            return false;
        }

        private static PageDTO _convertToPageDTO(Page page)
        {
            if (page == null)
            {
                return null;
            }

            return _mapper.Map<PageDTO>(page);
        }

        private static Page _convertToPage(PageDTO pageDTO)
        {
            if (pageDTO == null)
            {
                return null;
            }

            return _mapper.Map<Page>(pageDTO);
        }
    }
}
