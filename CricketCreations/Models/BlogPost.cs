using AutoMapper;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        //[Required]
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string Image { get; set; }
        public User User { get; set; }
        [Required]
        public int UserId { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<BlogPost, BlogPostDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<BlogPost>> GetAll()
        {
            List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetAll();
            return blogPostDTOs.Select(b => convertToBlogPost(b)).ToList();
        }
        public static async Task<BlogPost> Create(BlogPost blogPost)
        {
            BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
            BlogPostDTO dto = await BlogPostDTO.Create(blogPostDTO);
            return convertToBlogPost(await BlogPostDTO.Create(dto));
        }
        private static BlogPost convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            return mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
        }
        private static BlogPostDTO convertToBlogPostDTO(BlogPost blogPost)
        {
            return mapper.Map<BlogPost, BlogPostDTO>(blogPost);
        }
    }
}
