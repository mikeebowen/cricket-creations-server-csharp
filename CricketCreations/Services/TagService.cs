using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Repositories;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CricketCreationsRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CricketCreations.Services
{
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        private static MapperConfiguration config = new MapperConfiguration(config =>
        {
            //config.CreateMap<BlogPost, BlogPostRepository>().ReverseMap();
            config
            .CreateMap<TagDTO, Tag>()
            .ForMember(t => t.BlogPosts, option => option.MapFrom(t => t.BlogPosts.Select(b => BlogPostService.ConvertToBlogPost(b))))
            .ReverseMap()
            .ForMember(t => t.BlogPosts, option => option.MapFrom(t => t.BlogPosts.Select(b => BlogPostService.ConvertToBlogPostDTO(b))));
        });
        private static IMapper mapper = config.CreateMapper();

        public async Task<ActionResult<ResponseBody<Tag>>> Create(string json, int blogPostId, int userId)
        {
            NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<TagDTO>();
            ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(json);

            if (errors.Count == 0)
            {
                TagDTO tagDTO = JsonConvert.DeserializeObject<TagDTO>(json);
                TagDTO createdTagDTO = await _tagRepository.Create(tagDTO, blogPostId, userId);
                Tag tag = ConvertToTag(createdTagDTO);
                return new ResponseBody<Tag>(tag, typeof(Tag).Name.ToString(), null);
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

        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _tagRepository.Delete(id);
        }
        public async Task<ActionResult<ResponseBody<Tag>>> Read(int id)
        {
            TagDTO tagDTO = await _tagRepository.Read(id);
            return new ResponseBody<Tag>(ConvertToTag(tagDTO), typeof(Tag).Name.ToString(), null);
        }

        public async Task<ActionResult<ResponseBody<List<Tag>>>> Read(string page, string count)
        {
            try
            {
                ResponseBody<List<Tag>> response;
                List<Tag> tags;
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                int tagCount = await _tagRepository.GetCount();
                bool inRange = tagCount - (pg * cnt) >= ((cnt * -1) + 1);

                if (tagCount > 0 && !inRange)
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }

                if (validPage && validCount)
                {
                    List<TagDTO> tagDTOs = await _tagRepository.Read(pg, cnt);
                    tags = tagDTOs.Select(b => ConvertToTag(b)).ToList();
                }
                else
                {
                    List<TagDTO> tagDTOs = await _tagRepository.Read();
                    tags = tagDTOs.Select(t => ConvertToTag(t)).ToList();
                }
                response = new ResponseBody<List<Tag>>(tags, typeof(Tag).Name.ToString(), tagCount);
                return response;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public Task<ActionResult<ResponseBody<List<Tag>>>> Read(string page, string count, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<ResponseBody<Tag>>> Update(string jsonString)
        {
            

            NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<TagDTO>();
            ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(jsonString);

            if (errors.Count == 0)
            {
                TagDTO tagDTO = JsonConvert.DeserializeObject<TagDTO>(jsonString);
                TagDTO updatedTagDTO = await _tagRepository.Update(tagDTO);
                Tag tag = ConvertToTag(updatedTagDTO);
                return new ResponseBody<Tag>(tag, typeof(Tag).Name.ToString(), null);
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
        public static TagDTO ConvertToTagDTO(Tag tag)
        {
            if (tag == null)
            {
                return null;
            }
            return mapper.Map<Tag, TagDTO>(tag);
        }
        public static Tag ConvertToTag(TagDTO tagDTO)
        {
            if (tagDTO == null)
            {
                return null;
            }
            return mapper.Map<TagDTO, Tag>(tagDTO);
        }
        //public async Task<List<TagService>> GetAll(int? id)
        //{
        //    List<TagRepository> tagDTOs = await TagRepository.GetAll();
        //    List<TagService> tags = tagDTOs.Select(td => ConvertToTag(td)).ToList();
        //    return tags;
        //}
        //public async Task<TagService> Create(TagService tag, int userId)
        //{
        //    TagRepository tagDTO = ConvertToTagDTO(tag);
        //    ICollection<BlogPostDTO> blogPostDTOs = tag.BlogPosts.Select(b => mapper.Map<BlogPost, BlogPostDTO>(b)).ToList();
        //    tagDTO.BlogPosts = blogPostDTOs;
        //    var newTagDTO = await TagRepository.Create(tagDTO);
        //    return ConvertToTag(newTagDTO);
        //}
        //public static TagRepository ConvertToTagDTO(TagService tag)
        //{
        //    if (tag == null)
        //    {
        //        return null;
        //    }
        //    return mapper.Map<TagService, TagRepository>(tag);
        //}
        //public static TagService ConvertToTag(TagRepository tagDTO)
        //{
        //    if (tagDTO == null)
        //    {
        //        return null;
        //    }
        //    return mapper.Map<TagRepository, TagService>(tagDTO);
        //}

        //public Task<TagService> GetById(int id, bool? include)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<int> GetCount()
        //{
        //    return await TagRepository.GetCount();
        //}

        //public async Task<List<TagService>> GetRange(int page, int count, int? id)
        //{
        //    List<TagRepository> tagDTOs = await TagRepository.GetRange(page, count);
        //    return tagDTOs.Select(t => ConvertToTag(t)).ToList();
        //}

        //public Task<TagService> Update(TagService t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
