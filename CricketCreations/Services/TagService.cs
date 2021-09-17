using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Repositories;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Models;

namespace CricketCreations.Services
{
    public class TagService : IDataService<TagService>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(config =>
        {
            config.CreateMap<Services.BlogPostService, BlogPostRepository>().ReverseMap();
            config
            .CreateMap<Services.TagService, TagRepository>()
            .ForMember(t => t.BlogPosts, option => option.Ignore())
            .ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<TagService>> GetAll(int? id)
        {
            List<TagRepository> tagDTOs = await TagRepository.GetAll();
            List<TagService> tags = tagDTOs.Select(td => ConvertToTag(td)).ToList();
            return tags;
        }
        public async Task<TagService> Create(TagService tag, int userId)
        {
            TagRepository tagDTO = ConvertToTagDTO(tag);
            ICollection<BlogPostDTO> blogPostDTOs = tag.BlogPosts.Select(b => mapper.Map<BlogPost, BlogPostDTO>(b)).ToList();
            tagDTO.BlogPosts = blogPostDTOs;
            var newTagDTO = await TagRepository.Create(tagDTO);
            return ConvertToTag(newTagDTO);
        }
        public static TagRepository ConvertToTagDTO(TagService tag)
        {
            if (tag == null)
            {
                return null;
            }
            return mapper.Map<TagService, TagRepository>(tag);
        }
        public static TagService ConvertToTag(TagRepository tagDTO)
        {
            if (tagDTO == null)
            {
                return null;
            }
            return mapper.Map<TagRepository, TagService>(tagDTO);
        }

        public Task<TagService> GetById(int id, bool? include)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCount()
        {
            return await TagRepository.GetCount();
        }

        public async Task<List<TagService>> GetRange(int page, int count, int? id)
        {
            List<TagRepository> tagDTOs = await TagRepository.GetRange(page, count);
            return tagDTOs.Select(t => ConvertToTag(t)).ToList();
        }

        public Task<TagService> Update(TagService t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
