using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CricketCreationsDatabase.Models;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CricketCreationsRepository.Models
{
    public class PageDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<Page, PageDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<PageDTO>> GetAll()
        {
            List<Page> Pages = await DatabaseManager.Instance.Page.ToListAsync();
            List<PageDTO> PageDTOs = Pages.ConvertAll(p => mapper.Map<PageDTO>(p));
            return PageDTOs;
        }
        public static async Task<PageDTO> GetById(int id)
        {
            Page page = await DatabaseManager.Instance.Page.FindAsync(id);
            return mapper.Map<PageDTO>(page);
        }
        public static async Task<PageDTO> Create(PageDTO PageDTO)
        {
            Page Page = mapper.Map<Page>(PageDTO);
            var newPage = await DatabaseManager.Instance.Page.AddAsync(Page);
            PageDTO newPageDTO = mapper.Map<PageDTO>(newPage.Entity);
            await DatabaseManager.Instance.SaveChangesAsync();
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
                    DatabaseManager.Instance.Page.Remove(page);
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
    }
}
