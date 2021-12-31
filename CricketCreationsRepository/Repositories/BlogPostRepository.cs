using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CricketCreationsRepository.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private static readonly MapperConfiguration _config = new MapperConfiguration(c =>
        {
            c.CreateMap<BlogPost, BlogPostDTO>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => _convertToTagDTO(t))))
            .ReverseMap()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(b => b.Tags.Select(t => _convertToTag(t))))
            .ForMember(dest => dest.Created, opt => opt.Ignore());
        });

        private static readonly MapperConfiguration _tagConfig = new MapperConfiguration(context =>
            context.CreateMap<Tag, TagDTO>().ForMember(t => t.BlogPosts, options => options.Ignore()).ReverseMap());

        private static readonly IMapper _mapper = _config.CreateMapper();
        private static readonly IMapper _tagMapper = _tagConfig.CreateMapper();

        private readonly IDatabaseManager _databaseManager;

        public BlogPostRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<List<BlogPostDTO>> Read()
        {
            List<BlogPost> blogPosts = await _databaseManager.Instance.BlogPost.Where(x => !x.Deleted && x.Published).Include(b => b.Tags).ToListAsync();
            return blogPosts.Select(b => _convertToBlogPostDTO(b)).ToList();
        }

        public async Task<List<BlogPostDTO>> Read(int page, int count)
        {
            List<BlogPost> blogPosts = await _databaseManager.Instance.BlogPost
                                        .Where(b => b.Deleted == false && b.Published == true)
                                        .OrderByDescending(s => s.LastUpdated)
                                        .Skip((page - 1) * count).Take(count)
                                        .Include(b => b.Tags)
                                        .ToListAsync();

            return blogPosts.Select(b => _convertToBlogPostDTO(b)).ToList();
        }

        public async Task<List<BlogPostDTO>> Read(int page, int count, int id)
        {
            List<BlogPost> blogPosts = await _databaseManager.Instance.BlogPost
                                        .Where(b => !b.Deleted && b.Published && b.User.Id == id)
                                        .OrderByDescending(s => s.LastUpdated)
                                        .Skip((page - 1) * count)
                                        .Take(count)
                                        .ToListAsync();

            return blogPosts.Select(b => _convertToBlogPostDTO(b)).ToList();
        }

        public async Task<List<BlogPostDTO>> AdminRead(int page, int count, int id)
        {
            List<BlogPost> blogPosts = await _databaseManager.Instance.BlogPost
                                        .Where(b => !b.Deleted && b.User.Id == id)
                                        .OrderByDescending(s => s.LastUpdated)
                                        .Skip((page - 1) * count)
                                        .Take(count)
                                        .ToListAsync();

            return blogPosts.Select(b => _convertToBlogPostDTO(b)).ToList();
        }

        public async Task<BlogPostDTO> Read(int id)
        {
            BlogPost blogPost = await _databaseManager.Instance.BlogPost.Where(b => b.Id == id && !b.Deleted && b.Published).FirstOrDefaultAsync();
            return _convertToBlogPostDTO(blogPost);
        }

        public async Task<BlogPostDTO> Create(BlogPostDTO blogPostDTO, int userId)
        {
            BlogPost blogPost = _convertToBlogPost(blogPostDTO);

            User user = await _databaseManager.Instance.User.FindAsync(userId);

            if (user.BlogPosts == null)
            {
                user.BlogPosts = new List<BlogPost>();
            }

            user.BlogPosts.Add(blogPost);
            blogPost.User = user;

            List<Tag> newTags = blogPostDTO.Tags.Select(t =>
            {
                Tag tag;
                if (t.Id != null)
                {
                    tag = _databaseManager.Instance.Tag.Find(t.Id);
                    return tag;
                }
                else
                {
                    tag = _convertToTag(t);
                    tag.User = user;
                    return tag;
                }
            }).ToList();

            blogPost.Tags = newTags;

            var blog = await _databaseManager.Instance.BlogPost.AddAsync(blogPost);

            await _databaseManager.Instance.SaveChangesAsync();
            return _convertToBlogPostDTO(blog.Entity);
        }

        public async Task<BlogPostDTO> Update(BlogPostDTO blogPostDto, int userId)
        {
            BlogPost blogPost = await _databaseManager.Instance.BlogPost.Where(bp => bp.Id == blogPostDto.Id).Include(b => b.Tags).FirstOrDefaultAsync();
            User user = await _databaseManager.Instance.User.FindAsync(userId);

            if (blogPost == null || user == null)
            {
                return null;
            }

            List<Tag> newTags = blogPostDto.Tags.Select(t =>
            {
                Tag tag;
                if (t.Id != null)
                {
                    tag = _databaseManager.Instance.Tag.Find(t.Id);
                    return tag;
                }
                else
                {
                    tag = _convertToTag(t);
                    tag.User = user;
                    return tag;
                }
            }).ToList();

            BlogPost updatedBlogPost = _convertToBlogPost(blogPostDto);
            PropertyEntry createdProperty = _databaseManager.Instance.Entry(blogPost).Property("Created");
            updatedBlogPost.Id = blogPost.Id;
            _databaseManager.Instance.Entry(blogPost).CurrentValues.SetValues(updatedBlogPost);
            if (createdProperty != null)
            {
                _databaseManager.Instance.Entry(blogPost).Property("Created").IsModified = false;
            }

            blogPost.Tags = newTags;
            user.Tags = user.Tags ?? new List<Tag>();
            user.Tags.AddRange(newTags);

            await _databaseManager.Instance.SaveChangesAsync();
            return _convertToBlogPostDTO(blogPost);
        }

        public async Task<bool> Delete(int id)
        {
            BlogPost blogPost = await _databaseManager.Instance.BlogPost.FindAsync(id);
            if (blogPost != null)
            {
                blogPost.Deleted = true;
                await _databaseManager.Instance.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> GetCount()
        {
            return await _databaseManager.Instance.BlogPost.Where(b => b.Deleted == false).CountAsync();
        }

        public async Task<int> GetCount(int id)
        {
            return await _databaseManager.Instance.BlogPost.Where(b => b.Deleted == false && b.User.Id == id).CountAsync();
        }

        private static BlogPostDTO _convertToBlogPostDTO(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return null;
            }

            BlogPostDTO blogPostDTO = _mapper.Map<BlogPost, BlogPostDTO>(blogPost);

            return blogPostDTO;
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

        private BlogPost _convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            BlogPost blogPost = _mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
            return blogPost;
        }
    }
}
