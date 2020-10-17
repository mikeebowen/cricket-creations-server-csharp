using AutoMapper;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        //public User User { get; set; }
        public int UserId { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<BlogPost, BlogPostDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<BlogPost>> GetAll()
        {
            List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetAll();
            return blogPostDTOs.Select(b => convertToBlogPost(b)).ToList();
        }
        private static BlogPost convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            return mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
        }
    }
}
