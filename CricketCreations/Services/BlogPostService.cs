using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Services
{
    public class BlogPostService : IDataService<BlogPostService>
    {
        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public UserService User { get; set; }
        public bool Published { get; set; }
        public List<TagService> Tags { get; set; } = new List<TagService>();
        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<BlogPostService, BlogPostDTO>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => TagService.ConvertToTagDTO(t))));

            c.CreateMap<BlogPostDTO, BlogPostService>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => TagService.ConvertToTag(t))));

            // c.CreateMap<Tag, TagDTO>().ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<BlogPostService>> GetAll(int? id)
        {
            List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetAll(id);
            return blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
        }
        public async Task<List<BlogPostService>> GetRange(int page, int count, int? id)
        {
            List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetRange(page, count, id);
            return blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
        }
        public async Task<BlogPostService> GetById(int id, bool? include)
        {
            BlogPostDTO blogPostDTO = await BlogPostDTO.GeyById(id);
            return ConvertToBlogPost(blogPostDTO);
        }
        public async Task<BlogPostService> Create(BlogPostService blogPost, int userId)
        {
            BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
            BlogPostDTO createdBlogPostDTO = await BlogPostDTO.Create(blogPostDTO, userId);
            return ConvertToBlogPost(createdBlogPostDTO);
        }
        public async Task<BlogPostService> Update(BlogPostService blogPost)
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
        public static BlogPostService ConvertToBlogPost(BlogPostDTO blogPostDTO)
        {
            if (blogPostDTO == null)
            {
                return null;
            }
            BlogPostService blogPost = mapper.Map<BlogPostDTO, BlogPostService>(blogPostDTO);
            return blogPost;
        }
        private static BlogPostDTO convertToBlogPostDTO(BlogPostService blogPost)
        {
            if (blogPost == null)
            {
                return null;
            }
            BlogPostDTO blogPostDTO = mapper.Map<BlogPostService, BlogPostDTO>(blogPost);
            return blogPostDTO;
        }
    }
}
