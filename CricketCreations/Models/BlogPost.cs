using AutoMapper;
using CricketCreations.interfaces;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class BlogPost: IDataModel<BlogPost>
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
        public List<Tag> Tags { get; set; } = new List<Tag>();
        private static MapperConfiguration config = new MapperConfiguration(c => {
            c.CreateMap<BlogPost, BlogPostDTO>().ReverseMap();
            c.CreateMap<Tag, TagDTO>().ReverseMap();
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
        public async Task<BlogPost> Create(BlogPost blogPost)
        {
            BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
            BlogPostDTO createdBlogPostDTO = await BlogPostDTO.Create(blogPostDTO);
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
            List<Tag> tags = blogPostDTO.TagDTOs.Select(t => mapper.Map<TagDTO, Tag>(t)).ToList();
            blogPost.Tags = tags;
            return blogPost;
        }
        private static BlogPostDTO convertToBlogPostDTO(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return null;
            }
            BlogPostDTO blogPostDTO = mapper.Map<BlogPost, BlogPostDTO>(blogPost);
            List<TagDTO> tagDTOs = blogPost.Tags.Select(t => mapper.Map<Tag, TagDTO>(t)).ToList();
            foreach(TagDTO tagDTO in tagDTOs)
            {
                if (tagDTO.CreatedBy == null || tagDTO.CreatedBy < 1)
                {
                    tagDTO.CreatedBy = blogPostDTO.UserId;
                }
            }
            blogPostDTO.TagDTOs = tagDTOs;
            return blogPostDTO;
        }
    }
}
