using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;

namespace CricketCreations.Models
{
    public class Tag
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(config =>
        {
            //config
            //.CreateMap<Tag, TagDTO>()
            //.ForMember(t => t.BlogPosts, option => option.MapFrom(src => src.BlogPosts))
            //.ReverseMap();
            //config.CreateMap<BlogPost, BlogPostDTO>().ReverseMap();
            config.CreateMap<Models.BlogPost, BlogPostDTO>().ReverseMap();
            config
            .CreateMap<Models.Tag, TagDTO>()
            .ForMember(t => t.BlogPostsDTOs, option => option.Ignore())
            .ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<Tag>> GetAll()
        {
            List<TagDTO> tagDTOs = await TagDTO.GetAll();
            List<Tag> tags = tagDTOs.Select(td => convertToTag(td)).ToList();
            return tags;
        }
        public static async Task<Tag> Create(Tag tag)
        {
            TagDTO tagDTO = convertToTagDTO(tag);
            ICollection<BlogPostDTO> blogPostDTOs = tag.BlogPosts.Select(b => mapper.Map<BlogPost, BlogPostDTO>(b)).ToList();
            tagDTO.BlogPostsDTOs = blogPostDTOs;
            var newTagDTO = await TagDTO.Create(tagDTO);
            return convertToTag(newTagDTO);
        }
        private static TagDTO convertToTagDTO(Tag tag)
        {
            if (tag == null)
            {
                return null;
            }
            return mapper.Map<Tag, TagDTO>(tag);
        }
        private static Tag convertToTag(TagDTO tagDTO)
        {
            if (tagDTO == null)
            {
                return null;
            }
            return mapper.Map<TagDTO, Tag>(tagDTO);
        }
    }
}
