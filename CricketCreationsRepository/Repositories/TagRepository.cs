using AutoMapper;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Interfaces;
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
    public class TagRepository : ITagRepository
    {
        private static MapperConfiguration config = new MapperConfiguration(config => config
        .CreateMap<Tag, TagDTO>()
        .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
        // .ForMember(dest => dest.BlogPosts, opt => opt.MapFrom(tag => tag.BlogPosts.Select(b => BlogPostDTO.ConvertToBlogPostDTO(b)))).MaxDepth(1)
        .ReverseMap());
        // .ForMember(dest => dest.BlogPosts, opt => opt.MapFrom(tag => tag.BlogPosts.Select(b => BlogPostDTO.ConvertToBlogPost(b)))).MaxDepth(1));



        private static IMapper mapper = config.CreateMapper();
        public async Task<List<TagDTO>> Read()
        {
            List<Tag> tags = await DatabaseManager.Instance.Tag.Where(t => t.Deleted == false).ToListAsync();
            List<TagDTO> tagDTOs = tags.Select(t => ConvertToTagDTO(t)).ToList();
            return tagDTOs;
        }
        public async Task<List<TagDTO>> Read(int page, int count)
        {
            List<Tag> tags = await DatabaseManager.Instance.Tag.Skip((page - 1) * count).Take(count).ToListAsync();
            return tags.Select(b => ConvertToTagDTO(b)).ToList();
        }
        public async Task<TagDTO> Create(TagDTO tagDTO, int blogPostId, int userId)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(userId);
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(blogPostId);
            Tag newTag = ConvertToTag(tagDTO);

            blogPost.Tags.Add(newTag);
            user.Tags.Add(newTag);
            newTag.User = user;
            newTag.BlogPosts.Add(blogPost);

            DatabaseManager.Instance.Tag.Add(newTag);
            await DatabaseManager.Instance.SaveChangesAsync();
            return ConvertToTagDTO(newTag);
        }
        public async Task<int> GetCount()
        {
            return await DatabaseManager.Instance.Tag.Where(t => t.Deleted == false).CountAsync();
        }
        public static TagDTO ConvertToTagDTO(Tag tag)
        {
            return mapper.Map<Tag, TagDTO>(tag);
        }
        public static Tag ConvertToTag(TagDTO tagDTO)
        {
            return mapper.Map<TagDTO, Tag>(tagDTO);
        }
    }
}
