using AutoMapper;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Repositories
{
    public class TagRepository
    {
        [Key]
        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Deleted { get; set; } = false;
        public string Name { get; set; }
        public ICollection<BlogPostDTO> BlogPosts { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(config => config
        .CreateMap<Tag, TagRepository>()
        .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
        // .ForMember(dest => dest.BlogPosts, opt => opt.MapFrom(tag => tag.BlogPosts.Select(b => BlogPostDTO.ConvertToBlogPostDTO(b)))).MaxDepth(1)
        .ReverseMap());
        // .ForMember(dest => dest.BlogPosts, opt => opt.MapFrom(tag => tag.BlogPosts.Select(b => BlogPostDTO.ConvertToBlogPost(b)))).MaxDepth(1));



        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<TagRepository>> GetAll()
        {
            List<Tag> tags = await DatabaseManager.Instance.Tag.Where(t => t.Deleted == false).ToListAsync();
            List<TagRepository> tagDTOs = tags.Select(t => ConvertToTagDTO(t)).ToList();
            return tagDTOs;
        }
        public static async Task<TagRepository> Create(TagRepository tagDTO)
        {
            Tag newTag = ConvertToTag(tagDTO);
            BlogPost blogPost = DatabaseManager.Instance.BlogPost.ToList().FirstOrDefault(bp => tagDTO.BlogPosts.Count > 0 && bp.Id == tagDTO.BlogPosts.First().Id);

            DatabaseManager.Instance.Tag.Add(newTag);
            await DatabaseManager.Instance.SaveChangesAsync();
            return ConvertToTagDTO(newTag);
        }
        public static async Task<int> GetCount()
        {
            return await DatabaseManager.Instance.Tag.Where(t => t.Deleted == false).CountAsync();
        }
        public static async Task<List<TagRepository>> GetRange(int page, int count)
        {
            List<Tag> tags = await DatabaseManager.Instance.Tag.Skip((page - 1) * count).Take(count).ToListAsync();
            return tags.Select(b => ConvertToTagDTO(b)).ToList();
        }
        public static TagRepository ConvertToTagDTO(Tag tag)
        {
            return mapper.Map<Tag, TagRepository>(tag);
        }
        public static Tag ConvertToTag(TagRepository tagDTO)
        {
            return mapper.Map<TagRepository, Tag>(tagDTO);
        }
    }
}
