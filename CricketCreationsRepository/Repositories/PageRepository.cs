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
        private static MapperConfiguration _config = new MapperConfiguration(c => c.CreateMap<Page, PageDTO>().ReverseMap());
        private static IMapper _mapper = _config.CreateMapper();

        public async Task<bool> Delete(int id)
        {
            Page page = await DatabaseManager.Instance.Page.FindAsync(id);
            if (page != null)
            {
                page.Deleted = true;
                await DatabaseManager.Instance.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> GetCount()
        {
            return await DatabaseManager.Instance.Page.Where(p => p.Deleted == false).CountAsync();
        }

        public Task<int> GetCount(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PageDTO>> Read()
        {
            List<Page> pages = await DatabaseManager.Instance.Page.Where(p => p.Deleted == false).ToListAsync();
            List<PageDTO> pageDTOs = pages.Select(p => _convertToPageDTO(p)).ToList();
            return pageDTOs;
        }

        public async Task<List<PageDTO>> Read(int page, int count)
        {
            List<Page> pages = await DatabaseManager.Instance.Page.Skip((page - 1) * count).Take(count).ToListAsync();
            return pages.Select(p => _convertToPageDTO(p)).ToList();
        }

        public async Task<List<PageDTO>> Read(int page, int count, int id)
        {
            List<Page> pages = await DatabaseManager.Instance.Page.Where(p => p.User.Id == id && p.Published).Skip((page - 1) * count).Take(count).ToListAsync();
            return pages.Select(p => _convertToPageDTO(p)).ToList();
        }

        public async Task<PageDTO> Read(int id)
        {
            Page page = await DatabaseManager.Instance.Page.FindAsync(id);
            return _convertToPageDTO(page);
        }

        public async Task<PageDTO> Update(PageDTO pageDTO)
        {
            Page page = await DatabaseManager.Instance.Page.FindAsync(pageDTO.Id);

            if (page != null)
            {
                Page updatedPage = _convertToPage(pageDTO);
                DatabaseManager.Instance.Entry(page).CurrentValues.SetValues(updatedPage);
                PropertyEntry propertyEntry = DatabaseManager.Instance.Entry(page).Property("Created");

                if (propertyEntry != null)
                {
                    DatabaseManager.Instance.Entry(page).Property("Created").IsModified = false;
                }
                await DatabaseManager.Instance.SaveChangesAsync();
                return _convertToPageDTO(page);
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
            User user = await DatabaseManager.Instance.User.FindAsync(userId);
            user.Pages.Add(page);
            page.User = user;

            DatabaseManager.Instance.Page.Add(page);
            await DatabaseManager.Instance.SaveChangesAsync();
            return _convertToPageDTO(page);
        }
    }
}
