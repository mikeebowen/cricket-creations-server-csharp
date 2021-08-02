using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class BlogPost : IDataModel<BlogPost>
    {
        private int id;
        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public User User { get; set; }
        public bool Published { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<BlogPost, BlogPostDTO>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => Tag.ConvertToTagDTO(t))));

            c.CreateMap<BlogPostDTO, BlogPost>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => Tag.ConvertToTag(t))));

            // c.CreateMap<Tag, TagDTO>().ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<BlogPost>> GetAll(int? id)
        {
            List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetAll(id);
            return blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
        }
        public async Task<List<BlogPost>> GetRange(int page, int count, int? id)
        {
            List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetRange(page, count, id);
            return blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
        }
        public async Task<BlogPost> GetById(int id, bool? include)
        {
            BlogPostDTO blogPostDTO = await BlogPostDTO.GeyById(id);
            return ConvertToBlogPost(blogPostDTO);
        }
        public async Task<BlogPost> Create(BlogPost blogPost, int userId)
        {
            BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
            BlogPostDTO createdBlogPostDTO = await BlogPostDTO.Create(blogPostDTO, userId);
            return ConvertToBlogPost(createdBlogPostDTO);
        }
        public async Task<BlogPost> Update(BlogPost blogPost)
        {
            BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
            BlogPostDTO updatedBlogPostDTO = await BlogPostDTO.Update(blogPostDTO);
            return ConvertToBlogPost(updatedBlogPostDTO);
        }
        public async Task<bool> Delete(int id)
        {
            return await BlogPostDTO.Delete(id);
        }
        public async Task<int> GetCount()
        {
            return await BlogPostDTO.GetCount();
        }
        public static BlogPost ConvertToBlogPost(BlogPostDTO blogPostDTO)
        {
            if (blogPostDTO == null)
            {
                return null;
            }
            BlogPost blogPost = mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
            return blogPost;
        }
        private static BlogPostDTO convertToBlogPostDTO(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return null;
            }
            BlogPostDTO blogPostDTO = mapper.Map<BlogPost, BlogPostDTO>(blogPost);
            return blogPostDTO;
        }
    }
}
