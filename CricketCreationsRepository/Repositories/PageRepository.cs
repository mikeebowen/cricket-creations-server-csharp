using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CricketCreationsDatabase.Models;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CricketCreationsRepository.Repositories
{
    public class PageRepository : IPageRepository
    {
        private IDatabaseManager _databaseManager;

        public PageRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        private static MapperConfiguration _config = new MapperConfiguration(c => c.CreateMap<Page, PageDTO>().ReverseMap());
        private static IMapper _mapper = _config.CreateMapper();

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
            List<Page> pages = await _databaseManager.Instance.Page.Skip((page - 1) * count).Take(count).ToListAsync();
            return pages.Select(p => _convertToPageDTO(p)).ToList();
        }

        public async Task<List<PageDTO>> Read(int page, int count, int id)
        {
            List<Page> pages = await _databaseManager.Instance.Page.Where(p => p.User.Id == id && p.Published).Skip((page - 1) * count).Take(count).ToListAsync();
            return pages.Select(p => _convertToPageDTO(p)).ToList();
        }

        public async Task<PageDTO> Read(int id)
        {
            Page page = await _databaseManager.Instance.Page.FindAsync(id);
            return _convertToPageDTO(page);
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

        public async Task<PageDTO> Create(PageDTO pageDTO, int userId)
        {
            Page page = _convertToPage(pageDTO);
            User user = await _databaseManager.Instance.User.FindAsync(userId);
            user.Pages = user.Pages ?? new List<Page>();
            user.Pages.Add(page);
            page.User = user;

            _databaseManager.Instance.Page.Add(page);
            await _databaseManager.Instance.SaveChangesAsync();
            return _convertToPageDTO(page);
        }
    }
}
