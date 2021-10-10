using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreations.Services;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Profiles
{
    public class TagProfile : Profile
    {
        IBlogPostService _blogPostService;
        public TagProfile(/*IBlogPostService blogPostService*/)
        {
            //_blogPostService = blogPostService;

            CreateMap<TagDTO, Tag>()
            //.ForMember(t => t.BlogPosts, option => option.MapFrom(t => t.BlogPosts.Select(b => _blogPostService.ConvertToBlogPost(b))))
            .ReverseMap();
            //.ForMember(t => t.BlogPosts, option => option.MapFrom(t => t.BlogPosts.Select(b => _blogPostService.ConvertToBlogPostDTO(b))));
        }
    }
}
