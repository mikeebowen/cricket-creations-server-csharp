using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CricketCreations.Services
{
    public class BlogPostService : IApiService<BlogPost>
    {
        IBlogPostRepository _blogPostRepository;
        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<BlogPost, BlogPostRepository>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => TagService.ConvertToTagDTO(t))));

            c.CreateMap<BlogPostRepository, BlogPost>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => TagService.ConvertToTag(t))));

            // c.CreateMap<Tag, TagDTO>().ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();

        public async Task<ActionResult<ResponseBody<List<BlogPost>>>> Read(string page, string count)
        {
            try
            {
                ResponseBody<List<BlogPost>> response;
                List<BlogPost> blogPosts;
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                int blogPostCount = await _blogPostRepository.GetCount();
                bool inRange = blogPostCount - (pg * cnt) >= ((cnt * -1) + 1);

                if (blogPostCount > 0 && !inRange)
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }

                if (validPage && validCount)
                {
                    List<BlogPostRepository> blogPostDTOs = await _blogPostRepository.Read(pg, cnt);
                    blogPosts = blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }
                response = new ResponseBody<List<BlogPost>>(blogPosts, typeof(BlogPost).Name.ToString(), blogPostCount);
                return response;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<ActionResult<ResponseBody<List<BlogPost>>>> Read(string page, string count, string userId)
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

                if ((blogPostCount > 0 && !inRange) || !validId || !validCount)
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }
                List<BlogPostRepository> blogPostDTOs = await _blogPostRepository.Read(pg, cnt, id);
                blogPosts = blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
                response = new ResponseBody<List<BlogPost>>(blogPosts, typeof(BlogPost).Name.ToString(), blogPostCount);
                return response;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult<ResponseBody<BlogPost>>> Read(int id)
        {
            try
            {
                BlogPostRepository blogPostDTO = await _blogPostRepository.Get(id);
                BlogPost element = ConvertToBlogPost(blogPostDTO);
                if (element != null)
                {
                    ResponseBody<BlogPost> response = new ResponseBody<BlogPost>(element, typeof(BlogPost).Name.ToString(), null);
                    return response;
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult<ResponseBody<BlogPost>>> Create(JsonElement json, int userId)
        {
            string jsonString = json.ToString();
            NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<BlogPostRepository>();
            ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(jsonString);

            if (errors.Count == 0)
            {
                BlogPostRepository blogPostDTO = JsonConvert.DeserializeObject<BlogPostRepository>(jsonString);
                BlogPostRepository createdBlogPostDTO = await _blogPostRepository.Create(blogPostDTO, userId);
                BlogPost blog = ConvertToBlogPost(createdBlogPostDTO);
                return new ResponseBody<BlogPost>(blog, typeof(BlogPost).Name.ToString(), null);
            }
            else
            {
                List<ErrorObject> errs = new List<ErrorObject>();
                errors.ToList().ForEach(e =>
                {
                    errs.Add(new ErrorObject() { Message = e.Kind.ToString(), Property = e.Property });
                });
                return new BadRequestObjectResult(errs);
            }
        }

        public Task<ActionResult<ResponseBody<BlogPost>>> Update(string jsonString)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
        public static BlogPost ConvertToBlogPost(BlogPostRepository blogPostDTO)
        {
            if (blogPostDTO == null)
            {
                return null;
            }
            BlogPost blogPost = mapper.Map<BlogPostRepository, BlogPost>(blogPostDTO);
            return blogPost;
        }
        private static BlogPostRepository convertToBlogPostDTO(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return null;
            }
            BlogPostRepository blogPostDTO = mapper.Map<BlogPost, BlogPostRepository>(blogPost);
            return blogPostDTO;
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
