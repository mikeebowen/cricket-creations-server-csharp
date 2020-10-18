using AutoMapper;
using CricketCreationsDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Models
{
    public class BlogPostDTO
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string Image { get; set; }
        public UserDTO User { get; set; }
        public int UserId { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<BlogPostDTO, BlogPost>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<BlogPostDTO>> GetAll()
        {
            List<BlogPost> blogPosts = await DatabaseManager.Instance.BlogPost.ToListAsync();
            return blogPosts.Select(b => convertToBlogPostDTO(b)).ToList();
        }
        public static async Task<BlogPostDTO> GeyById(int id)
        {
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(id);
            return convertToBlogPostDTO(blogPost);
        }
        public static async Task<BlogPostDTO> Create(BlogPostDTO blogPostDTO)
        {
            BlogPost blogPost = convertToBlogPost(blogPostDTO);
            var blog = await DatabaseManager.Instance.BlogPost.AddAsync(blogPost);
            await DatabaseManager.Instance.SaveChangesAsync();
            return convertToBlogPostDTO(blog.Entity);

        }
        private static BlogPostDTO convertToBlogPostDTO(BlogPost blogPost)
        {
            return mapper.Map<BlogPost, BlogPostDTO>(blogPost);
        }
        private static BlogPost convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            return mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
        }
    }
}
