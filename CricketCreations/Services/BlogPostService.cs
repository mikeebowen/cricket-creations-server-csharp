using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CricketCreations.Services
{
    public class BlogPostService : IApiService<BlogPost>
    {
        IBlogPostRepository _blogPostRepository;
        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        //public int? Id { get; set; }
        //public DateTime Created { get; set; }
        //public DateTime LastUpdated { get; set; }
        //public string Title { get; set; }
        //public string Content { get; set; }
        //public string Image { get; set; }
        //public UserService User { get; set; }
        //public bool Published { get; set; }
        //public List<TagService> Tags { get; set; } = new List<TagService>();
        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<BlogPost, BlogPostDTO>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => TagService.ConvertToTagDTO(t))));

            c.CreateMap<BlogPostDTO, BlogPost>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => TagService.ConvertToTag(t))));

            // c.CreateMap<Tag, TagDTO>().ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();

        public Task<ActionResult<ResponseBody<List<BlogPost>>>> Get(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<ResponseBody<List<BlogPost>>>> Get(string page, string count, string userId)
        {
            try
            {
                ResponseBody<List<BlogPost>> response;
                List<BlogPost> blogPosts;
                bool validId = int.TryParse(userId, out int id);
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                int blogPostCount = await _blogPostRepository.GetCount();
                bool inRange = blogPostCount - (pg * cnt) >= ((cnt * -1) + 1);

                if (blogPostCount > 0 && !inRange)
                {
                    return new StatusCodeResult(416);
                }

                if (validPage && validCount)
                {
                    if (validId)
                    {
                        List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.GetRange(pg, cnt, id);
                        blogPosts = blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
                    }
                    else
                    {
                        List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.GetRange(pg, cnt, null);
                        blogPosts = blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
                    }
                }
                else
                {
                    if (validId)
                    {
                        List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.GetAll(id);
                        blogPosts = blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
                    }
                    else
                    {
                        List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.GetAll(null);
                        blogPosts = blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
                    }

                }
                response = new ResponseBody<List<BlogPost>>(blogPosts, typeof(BlogPost).Name.ToString(), blogPostCount);
                return response;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public Task<BlogPost> GetById(int id, bool? include)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ResponseBody<BlogPost>>> Post(JsonElement json, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ResponseBody<BlogPost>>> Patch(string jsonString)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
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

        Task<ActionResult<ResponseBody<BlogPost>>> IApiService<BlogPost>.GetById(int id, bool? include)
        {
            throw new NotImplementedException();
        }
        //public async Task<List<BlogPostService>> GetAll(int? id)
        //{
        //    List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.GetAll(id);
        //    return blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
        //}
        //public async Task<List<BlogPostService>> GetRange(int page, int count, int? id)
        //{
        //    List<BlogPostDTO> blogPostDTOs = await BlogPostDTO.GetRange(page, count, id);
        //    return blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
        //}
        //public async Task<BlogPostService> GetById(int id, bool? include)
        //{
        //    BlogPostDTO blogPostDTO = await BlogPostDTO.GeyById(id);
        //    return ConvertToBlogPost(blogPostDTO);
        //}
        //public async Task<BlogPostService> Create(BlogPostService blogPost, int userId)
        //{
        //    BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
        //    BlogPostDTO createdBlogPostDTO = await BlogPostDTO.Create(blogPostDTO, userId);
        //    return ConvertToBlogPost(createdBlogPostDTO);
        //}
        //public async Task<BlogPostService> Update(BlogPostService blogPost)
        //{
        //    BlogPostDTO blogPostDTO = convertToBlogPostDTO(blogPost);
        //    BlogPostDTO updatedBlogPostDTO = await BlogPostDTO.Update(blogPostDTO);
        //    return ConvertToBlogPost(updatedBlogPostDTO);
        //}
        //public async Task<bool> Delete(int id)
        //{
        //    return await BlogPostDTO.Delete(id);
        //}
        //public async Task<int> GetCount()
        //{
        //    return await BlogPostDTO.GetCount();
        //}
        //public static BlogPostService ConvertToBlogPost(BlogPostDTO blogPostDTO)
        //{
        //    if (blogPostDTO == null)
        //    {
        //        return null;
        //    }
        //    BlogPostService blogPost = mapper.Map<BlogPostDTO, BlogPostService>(blogPostDTO);
        //    return blogPost;
        //}
        //private static BlogPostDTO convertToBlogPostDTO(BlogPostService blogPost)
        //{
        //    if (blogPost == null)
        //    {
        //        return null;
        //    }
        //    BlogPostDTO blogPostDTO = mapper.Map<BlogPostService, BlogPostDTO>(blogPost);
        //    return blogPostDTO;
        //}
    }
}
