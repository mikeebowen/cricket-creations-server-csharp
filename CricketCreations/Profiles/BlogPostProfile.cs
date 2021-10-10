using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Profiles
{
    public class BlogPostProfile : Profile
    {
        ITagService _tagService;
        IMapper _mapper;
        public BlogPostProfile(/*ITagService tagService*/)
        {
            //_tagService = tagService;

            CreateMap<BlogPost, BlogPostDTO>();
            //.ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => _mapper.Map<Tag, TagDTO>(t))));

            CreateMap<BlogPostDTO, BlogPost>();
            //.ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => _tagService.ConvertToTag(t))));
        }
    }
}
