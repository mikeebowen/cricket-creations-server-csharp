using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;
using CricketCreations.Interfaces;

namespace CricketCreations.Services
{
    public class TagService : IDataService<TagService>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPostService> BlogPosts { get; set; } = new List<BlogPostService>();
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(config =>
        {
            config.CreateMap<Services.BlogPostService, BlogPostDTO>().ReverseMap();
            config
            .CreateMap<Services.TagService, TagDTO>()
            .ForMember(t => t.BlogPosts, option => option.Ignore())
            .ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<TagService>> GetAll(int? id)
        {
            List<TagDTO> tagDTOs = await TagDTO.GetAll();
            List<TagService> tags = tagDTOs.Select(td => ConvertToTag(td)).ToList();
            return tags;
        }
        public async Task<TagService> Create(TagService tag, int userId)
        {
            TagDTO tagDTO = ConvertToTagDTO(tag);
            ICollection<BlogPostDTO> blogPostDTOs = tag.BlogPosts.Select(b => mapper.Map<BlogPostService, BlogPostDTO>(b)).ToList();
            tagDTO.BlogPosts = blogPostDTOs;
            var newTagDTO = await TagDTO.Create(tagDTO);
            return ConvertToTag(newTagDTO);
        }
        public static TagDTO ConvertToTagDTO(TagService tag)
        {
            if (tag == null)
            {
                return null;
            }
            return mapper.Map<TagService, TagDTO>(tag);
        }
        public static TagService ConvertToTag(TagDTO tagDTO)
        {
            if (tagDTO == null)
            {
                return null;
            }
            return mapper.Map<TagDTO, TagService>(tagDTO);
        }

        public Task<TagService> GetById(int id, bool? include)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCount()
        {
            return await TagDTO.GetCount();
        }

        public async Task<List<TagService>> GetRange(int page, int count, int? id)
        {
            List<TagDTO> tagDTOs = await TagDTO.GetRange(page, count);
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
