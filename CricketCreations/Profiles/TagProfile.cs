using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Models;

namespace CricketCreations.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile(/*IBlogPostService blogPostService*/)
        {
            // _blogPostService = blogPostService;

            CreateMap<TagDTO, Tag>()
            // .ForMember(t => t.BlogPosts, option => option.MapFrom(t => t.BlogPosts.Select(b => _blogPostService.ConvertToBlogPost(b))))
            .ReverseMap();
            // .ForMember(t => t.BlogPosts, option => option.MapFrom(t => t.BlogPosts.Select(b => _blogPostService.ConvertToBlogPostDTO(b))));
        }
    }
}
