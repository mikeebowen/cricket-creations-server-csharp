using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;
using CricketCreations.Interfaces;

namespace CricketCreations.Models
{
    public class Tag : IDataModel<Tag>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(config =>
        {
            config.CreateMap<Models.BlogPost, BlogPostDTO>().ReverseMap();
            config
            .CreateMap<Models.Tag, TagDTO>()
            .ForMember(t => t.BlogPosts, option => option.Ignore())
            .ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<Tag>> GetAll(int? id)
        {
            List<TagDTO> tagDTOs = await TagDTO.GetAll();
            List<Tag> tags = tagDTOs.Select(td => ConvertToTag(td)).ToList();
            return tags;
        }
        public async Task<Tag> Create(Tag tag, int userId)
        {
            TagDTO tagDTO = ConvertToTagDTO(tag);
            ICollection<BlogPostDTO> blogPostDTOs = tag.BlogPosts.Select(b => mapper.Map<BlogPost, BlogPostDTO>(b)).ToList();
            tagDTO.BlogPosts = blogPostDTOs;
            var newTagDTO = await TagDTO.Create(tagDTO);
            return ConvertToTag(newTagDTO);
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

        public Task<Tag> GetById(int id, bool? include)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCount()
        {
            return await TagDTO.GetCount();
        }

        public async Task<List<Tag>> GetRange(int page, int count, int? id)
        {
            List<TagDTO> tagDTOs = await TagDTO.GetRange(page, count);
            return tagDTOs.Select(t => ConvertToTag(t)).ToList();
        }

        public Task<Tag> Update(Tag t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
