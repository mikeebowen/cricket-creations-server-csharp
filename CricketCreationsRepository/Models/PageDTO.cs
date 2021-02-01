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

namespace CricketCreationsRepository.Models
{
    public class PageDTO
    {
        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; } = false;
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<Page, PageDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<PageDTO>> GetAll()
        {
            List<Page> Pages = await DatabaseManager.Instance.Page.Where(p => p.Deleted == false).ToListAsync();
            List<PageDTO> PageDTOs = Pages.ConvertAll(p => mapper.Map<PageDTO>(p));
            return PageDTOs;
        }
        public static async Task<PageDTO> GetById(int id)
        {
            Page page = await DatabaseManager.Instance.Page.FindAsync(id);
            return mapper.Map<PageDTO>(page);
        }
        public static async Task<PageDTO> Create(PageDTO pageDTO)
        {
            Page page = mapper.Map<Page>(pageDTO);
            var newPage = await DatabaseManager.Instance.Page.AddAsync(page);
            await DatabaseManager.Instance.SaveChangesAsync();
            PageDTO newPageDTO = mapper.Map<PageDTO>(newPage.Entity);
            return newPageDTO;
        }
        public static async Task<PageDTO> Update(PageDTO pageDTO)
        {
            Page page = await DatabaseManager.Instance.Page.FindAsync(pageDTO.Id);
            if (page != null)
            {
                Page updatedPage = mapper.Map<Page>(pageDTO);
                PropertyInfo[] propertyInfos = page.GetType().GetProperties();
                foreach (PropertyInfo property in propertyInfos)
                {
                    var val = property.GetValue(updatedPage);
                    if (val != null)
                    {
                        if (!(property.Name != "Id" && int.TryParse(val.ToString(), out int res) && res < 1) && property.Name != "Created")
                        {
                            property.SetValue(page, val);
                        }
                    }
                }
                await DatabaseManager.Instance.SaveChangesAsync();
                return mapper.Map<PageDTO>(page);
            }
            return null;
        }
        public static async Task<bool> Delete(int id)
        {
            try
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
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
        }
        public static async Task<int> GetCount()
        {
            return await DatabaseManager.Instance.Page.CountAsync();
        }
        public static async Task<List<PageDTO>> GetRange(int page, int count, int? id)
        {
            List<Page> pages = await DatabaseManager.Instance.Page.Where(p => p.Deleted == false).Skip((page - 1) * count).Take(count).ToListAsync();
            return pages.Select(p => mapper.Map<PageDTO>(p)).ToList();
        }
    }
}
