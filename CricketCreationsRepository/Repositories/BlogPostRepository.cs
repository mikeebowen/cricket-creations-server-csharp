using AutoMapper;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<BlogPost, BlogPostDTO>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => _convertToTagDTO(t))))
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => _convertToTag(t))));
        });
        private static MapperConfiguration tagConfig = new MapperConfiguration(context =>
            context.CreateMap<Tag, TagDTO>().ForMember(t => t.BlogPosts, options => options.Ignore()).ReverseMap()
        );
        private static IMapper _mapper = config.CreateMapper();
        private static IMapper _tagMapper = tagConfig.CreateMapper();
        public async Task<List<BlogPostDTO>> Read()
        {
            List<BlogPost> blogPosts = await DatabaseManager.Instance.BlogPost.Where(x => !x.Deleted && x.Published).Include(b => b.Tags).ToListAsync();
            return blogPosts.Select(b => _convertToBlogPostDTO(b)).ToList();
        }
        public async Task<List<BlogPostDTO>> Read(int page, int count)
        {
            List<BlogPost> blogPosts = await DatabaseManager.Instance.BlogPost
                                        .Where(b => b.Deleted == false && b.Published == true)
                                        .OrderByDescending(s => s.LastUpdated)
                                        .Skip((page - 1) * count).Take(count)
                                        .Include(b => b.Tags)
                                        .ToListAsync();

            return blogPosts.Select(b => _convertToBlogPostDTO(b)).ToList();
        }
        public async Task<List<BlogPostDTO>> Read(int page, int count, int id)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(id);
            List<BlogPost> blogPosts = user.BlogPosts
                                        .Where(b => !b.Deleted && b.Published)
                                        .OrderByDescending(s => s.LastUpdated)
                                        .Skip((page - 1) * count)
                                        .Take(count)
                                        .ToList();

            return blogPosts.Select(b => _convertToBlogPostDTO(b)).ToList();
        }
        public async Task<BlogPostDTO> Read(int id)
        {
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(id);
            return _convertToBlogPostDTO(blogPost);
        }
        public async Task<BlogPostDTO> Create(BlogPostDTO blogPostDTO, int userId)
        {
            BlogPost blogPost = _convertToBlogPost(blogPostDTO);

            User user = await DatabaseManager.Instance.User.FirstAsync(u => u.Id == userId);
            user.BlogPosts.Add(blogPost);
            blogPost.User = user;

            var blog = await DatabaseManager.Instance.BlogPost.AddAsync(blogPost);
            await DatabaseManager.Instance.SaveChangesAsync();
            return _convertToBlogPostDTO(blog.Entity);
        }
        public async Task<BlogPostDTO> Update(BlogPostDTO blogPostDto, int userId)
        {
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.Where(bp => bp.Id == blogPostDto.Id).Include(b => b.Tags).FirstOrDefaultAsync();
            if (blogPost == null)
            {
                return null;
            }
            List<Tag> newTags = blogPostDto.Tags.Select(t =>
            {
                Tag tag;
                if (t.Id != null)
                {
                    tag = DatabaseManager.Instance.Tag.Where(tt => tt.Id == t.Id).FirstOrDefault();
                    return tag;
                }
                else
                {
                    tag = _convertToTag(t);
                    return tag;
                }
            }).ToList();

            if (blogPost != null)
            {
                BlogPost updatedBlogPost = _convertToBlogPost(blogPostDto);
                DatabaseManager.Instance.Entry(blogPost).CurrentValues.SetValues(updatedBlogPost);
                blogPost.Tags = newTags;
                PropertyEntry property = DatabaseManager.Instance.Entry(blogPost).Property("Created");

                if (property != null)
                {
                    DatabaseManager.Instance.Entry(blogPost).Property("Created").IsModified = false;
                }
                await DatabaseManager.Instance.SaveChangesAsync();
                return _convertToBlogPostDTO(blogPost);
            }
            return null;
        }
        public async Task<bool> Delete(int id)
        {
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(id);
            if (blogPost != null)
            {
                blogPost.Deleted = true;
                await DatabaseManager.Instance.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<int> GetCount()
        {
            return await DatabaseManager.Instance.BlogPost.Where(b => b.Deleted == false).CountAsync();
        }

        public async Task<int> GetCount(int id)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(id);
            return user.BlogPosts.Where(b => b.Deleted == false).Count();
        }

        private static BlogPostDTO _convertToBlogPostDTO(BlogPost blogPost)
        {
            BlogPostDTO blogPostDTO = _mapper.Map<BlogPost, BlogPostDTO>(blogPost);

            return blogPostDTO;
        }
        private BlogPost _convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            BlogPost blogPost = _mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
            return blogPost;
        }
        private static Tag _convertToTag(TagDTO tagDTO)
        {
            if (tagDTO == null)
            {
                return null;
            }
            return _tagMapper.Map<Tag>(tagDTO);
        }
        private static TagDTO _convertToTagDTO(Tag tag)
        {
            if (tag == null)
            {
                return null;
            }
            return _tagMapper.Map<TagDTO>(tag);
        }
    }
}
