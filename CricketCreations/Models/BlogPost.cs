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
        private int? id;
        public int? Id { get; set; }
        public Nullable<DateTime> Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public User User { get; set; }
        public int? UserId
        {
            get
            {
                return id;
            }
            set
            {
                if (value > 0)
                {
                    id = value;
                }
            }
        }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<BlogPost, BlogPostDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<BlogPost>> GetAll()
        {
            List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetAll();
            return blogPostDTOs.Select(b => convertToBlogPost(b)).ToList();
        }
        public static async Task<List<BlogPost>> GetRange(int page, int count)
        {
            List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetRange(page, count);
            return blogPostDTOs.Select(b => convertToBlogPost(b)).ToList();
        }
        public static async Task<BlogPost> GetById(int id)
        {
            BlogPostDTO blogPostDTO = await BlogPostDTO.GeyById(id);
            return convertToBlogPost(blogPostDTO);
        }
        public static async Task<BlogPost> Create(BlogPost blogPost)
        {
            BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
            return convertToBlogPost(await BlogPostDTO.Create(blogPostDTO));
        }
        public static async Task<BlogPost> Update(BlogPost blogPost)
        {
            BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
            BlogPostDTO updatedBlogPostDTO = await BlogPostDTO.Update(blogPostDTO);
            return convertToBlogPost(updatedBlogPostDTO);
        }
        public static async Task<bool> Delete(int id)
        {
            return await BlogPostDTO.Delete(id);
        }
        private static BlogPost convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            if (blogPostDTO == null)
            {
                return null;
            }
            return mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
        }
        private static BlogPostDTO convertToBlogPostDTO(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return null;
            }
            return mapper.Map<BlogPost, BlogPostDTO>(blogPost);
        }
    }
}
