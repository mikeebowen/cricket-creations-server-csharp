using AutoMapper;
using CricketCreationsDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Models
{
    public class BlogPostDTO
    {
        [Key]
        public int? Id { get; set; }
        public Nullable<DateTime> Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public UserDTO User { get; set; }
        public int? UserId { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<BlogPost, BlogPostDTO > ()
            .ForMember(dest => dest.User, opt => opt.Ignore()));
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<BlogPostDTO>> GetAll()
        {
            List<BlogPost> blogPosts = await DatabaseManager.Instance.BlogPost.ToListAsync();
            return blogPosts.Select(b => ConvertToBlogPostDTO(b)).ToList();
        }
        public static async Task<List<BlogPostDTO>> GetRange(int page, int count)
        {
            List<BlogPost> blogPosts = await DatabaseManager.Instance.BlogPost.Skip((page - 1) * count).Take(count).ToListAsync();
            return blogPosts.Select(b => ConvertToBlogPostDTO(b)).ToList();
        }
        public static async Task<BlogPostDTO> GeyById(int id)
        {
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(id);
            return ConvertToBlogPostDTO(blogPost);
        }
        public static async Task<BlogPostDTO> Create(BlogPostDTO blogPostDTO)
        {
            BlogPost blogPost = convertToBlogPost(blogPostDTO);
            var blog = await DatabaseManager.Instance.BlogPost.AddAsync(blogPost);
            await DatabaseManager.Instance.SaveChangesAsync();
            return ConvertToBlogPostDTO(blog.Entity);
        }
        public static async Task<BlogPostDTO> Update(BlogPostDTO blogPostDto)
        {
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(blogPostDto.Id);
            if (blogPost != null)
            {
                BlogPost updatedBlogPost = mapper.Map<BlogPostDTO, BlogPost>(blogPostDto);
                PropertyInfo[] propertyInfos = blogPost.GetType().GetProperties();
                foreach (PropertyInfo property in propertyInfos)
                {
                    var val = property.GetValue(updatedBlogPost);
                    if (val != null)
                    {
                        if (!((property.Name == "UserId" || property.Name == "Id") && int.TryParse(val.ToString(), out int res) && res < 1) && property.Name != "Created")
                        {
                            property.SetValue(blogPost, val);
                        }
                    }
                }
                await DatabaseManager.Instance.SaveChangesAsync();
                return ConvertToBlogPostDTO(blogPost);
            }
            return null;
        }
        public static async Task<bool> Delete(int id)
        {
            try
            {
                BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(id);
                if (blogPost != null)
                {
                    DatabaseManager.Instance.BlogPost.Remove(blogPost);
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
        public static BlogPostDTO ConvertToBlogPostDTO(BlogPost blogPost)
        {
            return mapper.Map<BlogPost, BlogPostDTO>(blogPost);
        }
        private static BlogPost convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            return mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
        }
    }
}
