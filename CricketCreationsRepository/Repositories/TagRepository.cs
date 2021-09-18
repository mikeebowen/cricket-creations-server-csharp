﻿using AutoMapper;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        private static MapperConfiguration config = new MapperConfiguration(config =>
        config.CreateMap<Tag, TagDTO>()
                .ForMember(dest => dest.BlogPosts, opt => opt.MapFrom(tag => tag.BlogPosts.Select(b => _convertToBlogPostDTO(b))))
                .ReverseMap()
                .ForMember(dest => dest.BlogPosts, opt => opt.MapFrom(tag => tag.BlogPosts.Select(b => _convertToBlogPost(b))))
        );
        private static MapperConfiguration config2 = new MapperConfiguration(config =>
            config.CreateMap<BlogPost, BlogPostDTO>().ForMember(dest => dest.Tags, opt => opt.Ignore()).ForMember(dest => dest.User, opt => opt.Ignore()).ReverseMap()
        );


        private static IMapper mapper = config.CreateMapper();
        private static IMapper blogPostMapper = config2.CreateMapper();
        public async Task<List<TagDTO>> Read()
        {
            List<Tag> tags = await DatabaseManager.Instance.Tag.Where(t => t.Deleted == false).ToListAsync();

            List<TagDTO> tagDTOs = tags.Select(t => _convertToTagDTO(t)).ToList();
            return tagDTOs;
        }
        public async Task<TagDTO> Read(int tagId)
        {
            Tag tag = await DatabaseManager.Instance.Tag.Where(tag => tag.Id == tagId).Include(t => t.BlogPosts).FirstAsync();
            return _convertToTagDTO(tag);
        }
        public async Task<List<TagDTO>> Read(int page, int count)
        {
            List<Tag> tags = await DatabaseManager.Instance.Tag.Skip((page - 1) * count).Take(count).ToListAsync();
            return tags.Select(b => _convertToTagDTO(b)).ToList();
        }
        public async Task<TagDTO> Create(TagDTO tagDTO, int blogPostId, int userId)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(userId);
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(blogPostId);
            Tag newTag = _convertToTag(tagDTO);

            blogPost.Tags.Add(newTag);
            user.Tags.Add(newTag);
            newTag.User = user;
            newTag.BlogPosts.Add(blogPost);

            DatabaseManager.Instance.Tag.Add(newTag);
            await DatabaseManager.Instance.SaveChangesAsync();
            TagDTO tagDTO1 = _convertToTagDTO(newTag);
            tagDTO1.BlogPosts = new List<BlogPostDTO>();
            return tagDTO1;
        }
        public async Task<int> GetCount()
        {
            return await DatabaseManager.Instance.Tag.Where(t => t.Deleted == false).CountAsync();
        }
        private TagDTO _convertToTagDTO(Tag tag)
        {
            return mapper.Map<Tag, TagDTO>(tag);
        }
        private Tag _convertToTag(TagDTO tagDTO)
        {
            return mapper.Map<TagDTO, Tag>(tagDTO);
        }
        private static BlogPost _convertToBlogPost(BlogPostDTO b)
        {
            return blogPostMapper.Map<BlogPostDTO, BlogPost>(b);
        }
        private static BlogPostDTO _convertToBlogPostDTO(BlogPost b)
        {
            return blogPostMapper.Map<BlogPost, BlogPostDTO>(b);
        }

        public async Task<List<TagDTO>> Read(int page, int count, int id)
        {
            List<Tag> tags = await DatabaseManager.Instance.Tag.Where(t => t.Id == id && t.Deleted == false).Skip((page - 1) * count).Take(count).ToListAsync();
            return tags.Select(b => _convertToTagDTO(b)).ToList();
        }

        public async Task<TagDTO> Update(TagDTO tagDTO)
        {
            Tag tag = await DatabaseManager.Instance.Tag.Where(tag => tag.Id == tagDTO.Id).Include(t => t.BlogPosts).FirstAsync();
            List<BlogPost> newBlogPosts = null;
            if (tagDTO.BlogPosts != null)
            {
                newBlogPosts = tagDTO.BlogPosts.Select(b =>
                {
                    BlogPost blogPost;
                    if (b.Id != null)
                    {
                        blogPost = DatabaseManager.Instance.BlogPost.Where(bb => bb.Id == b.Id).First();
                        return blogPost;
                    }
                    else
                    {
                        blogPost = _convertToBlogPost(b);
                        return blogPost;
                    }
                }).ToList();
            }

            if (tag != null)
            {
                Tag updatedTag = _convertToTag(tagDTO);
                DatabaseManager.Instance.Entry(tag).CurrentValues.SetValues(updatedTag);
                if (newBlogPosts != null)
                {
                    tag.BlogPosts = newBlogPosts;
                };
                PropertyEntry property = DatabaseManager.Instance.Entry(tag).Property("Created");

                if (property != null)
                {
                    DatabaseManager.Instance.Entry(tag).Property("Created").IsModified = false;
                }
                await DatabaseManager.Instance.SaveChangesAsync();
                return _convertToTagDTO(tag);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            Tag tag = await DatabaseManager.Instance.Tag.FindAsync(id);
            if (tag != null)
            {
                tag.Deleted = true;
                await DatabaseManager.Instance.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}