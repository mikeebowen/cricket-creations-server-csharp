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
using CricketCreationsRepository.Models;

namespace CricketCreations.Services
{
    public class BlogPostService : IBlogPostService
    {
        private IBlogPostRepository _blogPostRepository;
        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<BlogPost, BlogPostDTO>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => TagService.ConvertToTagDTO(t))));

            c.CreateMap<BlogPostDTO, BlogPost>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => TagService.ConvertToTag(t))));

            // c.CreateMap<Tag, TagDTO>().ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();

        public async Task<IActionResult> Read(string page, string count)
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
                    List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.Read(pg, cnt);
                    blogPosts = blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }
                //response = new ResponseBody<List<BlogPost>>(blogPosts, typeof(BlogPost).Name.ToString(), blogPostCount);
                return new OkObjectResult(new ResponseBody<List<BlogPost>>(blogPosts, typeof(BlogPost).Name.ToString(), blogPostCount));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<IActionResult> Read(string page, string count, string userId)
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
                List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.Read(pg, cnt, id);
                blogPosts = blogPostDTOs.Select(b => ConvertToBlogPost(b)).ToList();
                return new OkObjectResult(new ResponseBody<List<BlogPost>>(blogPosts, typeof(BlogPost).Name.ToString(), blogPostCount));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> Read(int id)
        {
            try
            {
                BlogPostDTO blogPostDTO = await _blogPostRepository.Read(id);
                BlogPost element = ConvertToBlogPost(blogPostDTO);
                if (element != null)
                {
                    return new OkObjectResult(new ResponseBody<BlogPost>(element, typeof(BlogPost).Name.ToString(), null));
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

        public async Task<IActionResult> Create(JsonElement json, int userId)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<BlogPostDTO>();
                ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(jsonString);

                if (errors.Count == 0)
                {
                    BlogPostDTO blogPostDTO = JsonConvert.DeserializeObject<BlogPostDTO>(jsonString);
                    BlogPostDTO createdBlogPostDTO = await _blogPostRepository.Create(blogPostDTO, userId);
                    BlogPost blog = ConvertToBlogPost(createdBlogPostDTO);
                    return new CreatedResult($"api/blogpost/{blog.Id}", blog);
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
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> Update(string jsonString)
        {
            try
            {
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<BlogPostDTO>();
                ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(jsonString);

                if (errors.Count == 0)
                {
                    BlogPostDTO blogPostDTO = JsonConvert.DeserializeObject<BlogPostDTO>(jsonString);
                    BlogPostDTO updatedBlogPostDTO = await _blogPostRepository.Update(blogPostDTO);
                    BlogPost blogPost = ConvertToBlogPost(updatedBlogPostDTO);
                    return new OkObjectResult(new ResponseBody<BlogPost>(blogPost, typeof(BlogPost).Name.ToString(), null));
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
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _blogPostRepository.Delete(id))
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
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
        public static BlogPostDTO ConvertToBlogPostDTO(BlogPost blogPost)
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
