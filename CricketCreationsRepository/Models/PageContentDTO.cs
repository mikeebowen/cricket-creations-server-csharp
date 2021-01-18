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
    public class PageContentDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<PageContent, PageContentDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<PageContentDTO>> GetAll()
        {
            List<PageContent> pageContents = await DatabaseManager.Instance.PageContent.ToListAsync();
            List<PageContentDTO> pageContentDTOs = pageContents.ConvertAll(p => mapper.Map<PageContentDTO>(p));
            return pageContentDTOs;
        }

        public static async Task<PageContentDTO> Create(PageContentDTO pageContentDTO)
        {
            PageContent pageContent = mapper.Map<PageContent>(pageContentDTO);
            var newPageContent = await DatabaseManager.Instance.PageContent.AddAsync(pageContent);
            PageContentDTO newPageContentDTO = mapper.Map<PageContentDTO>(newPageContent.Entity);
            await DatabaseManager.Instance.SaveChangesAsync();
            return newPageContentDTO;
        }
        public static async Task<PageContentDTO> Update(PageContentDTO pageContentDTO)
        {
            PageContent pageContent = await DatabaseManager.Instance.PageContent.FindAsync(pageContentDTO.Id);
            if (pageContent != null)
            {
                PageContent updatedPageContent = mapper.Map<PageContent>(pageContentDTO);
                PropertyInfo[] propertyInfos = pageContent.GetType().GetProperties();
                foreach (PropertyInfo property in propertyInfos)
                {
                    var val = property.GetValue(updatedPageContent);
                    if (val != null)
                    {
                        if (!(property.Name != "Id" && int.TryParse(val.ToString(), out int res) && res < 1) && property.Name != "Created")
                        {
                            property.SetValue(pageContent, val);
                        }
                    }
                }
                await DatabaseManager.Instance.SaveChangesAsync();
                return mapper.Map<PageContentDTO>(pageContent);
            }
            return null;
        }
        public static async Task<bool> Delete(int id)
        {
            try
            {
                PageContent pageContent = await DatabaseManager.Instance.PageContent.FindAsync(id);
                if (pageContent != null)
                {
                    DatabaseManager.Instance.PageContent.Remove(pageContent);
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
