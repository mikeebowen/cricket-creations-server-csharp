using AutoMapper;
using CricketCreationsDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Models
{
    public class TagDTO
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPostDTO> BlogPostsDTOs { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(config => config
        .CreateMap<Tag, TagDTO>()
        .ForMember(src => src.Id, options => options.Ignore())
        .ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<TagDTO> Create(TagDTO tagDTO)
        {
            Tag newTag = convertToTag(tagDTO);
            BlogPost blogPost = DatabaseManager.Instance.BlogPost.ToList().Find(bp => bp.Id == tagDTO.BlogPostsDTOs.FirstOrDefault().Id);
            newTag.BlogPostTags = new List<BlogPostTag>
            {
               new BlogPostTag
                {
                    Tag = newTag,
                    BlogPost = blogPost
                }
            };
            DatabaseManager.Instance.Tag.Add(newTag);
            await DatabaseManager.Instance.SaveChangesAsync();
            return ConvertToTagDTO(newTag);
        }
        public static TagDTO ConvertToTagDTO(Tag tag)
        {
            return mapper.Map<Tag, TagDTO>(tag);
        }
        private static Tag convertToTag(TagDTO tagDTO)
        {
            return mapper.Map<TagDTO, Tag>(tagDTO);
        }
    }
}
