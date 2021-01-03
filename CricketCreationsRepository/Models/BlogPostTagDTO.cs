using AutoMapper;
using CricketCreationsDatabase.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CricketCreationsRepository.Models
{
    class BlogPostTagDTO
    {
        MapperConfiguration config = new MapperConfiguration(config => {
            config.CreateMap<BlogPostTagDTO, BlogPostTag>()
            .ReverseMap();
        });
        [Key]
        public int? Id { get; set; }
        public int TagId { get; set; }
        public int BlogPostId { get; set; }
    }
}
