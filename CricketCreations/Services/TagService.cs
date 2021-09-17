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

namespace CricketCreations.Services
{
    public class TagService : IApiService<Tag>
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

        public Task<ActionResult<ResponseBody<Tag>>> Create(JsonElement json, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<ResponseBody<BlogPost>>> Read(int id)
        {
            throw new NotImplementedException();
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

        public Task<ActionResult<ResponseBody<Tag>>> Update(string jsonString)
        {
            throw new NotImplementedException();
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
