using AutoMapper;
using CricketCreationsDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Models
{
    public class BlogPostDTO
    {
        [Key]
        public int? Id { get; set; }
        public Nullable<DateTime> Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public UserDTO User { get; set; }
        public int? UserId { get; set; }
        public bool Published { get; set; } = false;
        public List<TagDTO> TagDTOs { get; set; } = new List<TagDTO>();
        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<BlogPost, BlogPostDTO>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ReverseMap();
            c.CreateMap<Tag, TagDTO>().ReverseMap();
        });
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<BlogPostDTO>> GetAll(int? id)
        {
            List<BlogPost> blogPosts;
            if (id == null)
            {
                blogPosts = await DatabaseManager.Instance.BlogPost.Where(x => x.Deleted == false).Include(b => b.BlogPostTags).ThenInclude(t => t.Tag).ToListAsync();
            }
            else
            {
                blogPosts = await DatabaseManager.Instance.BlogPost.Where(b => b.UserId == id && b.Deleted == false).Include(b => b.BlogPostTags).ThenInclude(t => t.Tag).ToListAsync();
            }
            return blogPosts.Select(b => ConvertToBlogPostDTO(b)).ToList();
        }
        public static async Task<List<BlogPostDTO>> GetRange(int page, int count, int? id)
        {
            List<BlogPost> blogPosts;
            if (id == null)
            {
                blogPosts = await DatabaseManager.Instance.BlogPost.Where(b =>  b.Deleted == false).Skip((page - 1) * count).Take(count).ToListAsync();
            }
            else
            {
                blogPosts = await DatabaseManager.Instance.BlogPost.Where(b => b.UserId == id && b.Deleted == false).OrderByDescending(s => s.LastUpdated).Skip((page - 1) * count).Take(count).ToListAsync();
            }
            return blogPosts.Select(b => ConvertToBlogPostDTO(b)).ToList();
        }
        public static async Task<BlogPostDTO> GeyById(int id)
        {
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(id);
            return ConvertToBlogPostDTO(blogPost);
        }
        public static async Task<BlogPostDTO> Create(BlogPostDTO blogPostDTO)
        {
            BlogPost blogPost = convertToBlogPost(blogPostDTO);
            List<int?> existingTagIds = blogPostDTO.TagDTOs.Select(t => t.Id).ToList();
            List<Tag> tags = DatabaseManager.Instance.Tag.ToList().FindAll(t => existingTagIds.Contains(t.Id));
            blogPost.BlogPostTags = tags.Select(t => new BlogPostTag { Tag = t, BlogPost = blogPost }).ToList();
            foreach (TagDTO tagDTO in blogPostDTO.TagDTOs)
            {
                if (tagDTO.Id == null)
                {
                    Tag tag = new Tag { Name = tagDTO.Name, UserId = blogPost.UserId };
                    BlogPostTag blogPostTag = new BlogPostTag { Tag = tag, BlogPost = blogPost };
                    blogPost.BlogPostTags.Add(blogPostTag);
                }
            }
            var blog = await DatabaseManager.Instance.BlogPost.AddAsync(blogPost);
            await DatabaseManager.Instance.SaveChangesAsync();
            return ConvertToBlogPostDTO(blog.Entity);
        }
        public static async Task<BlogPostDTO> Update(BlogPostDTO blogPostDto)
        {
            BlogPost blogPost = await DatabaseManager.Instance.BlogPost.FindAsync(blogPostDto.Id);
            if (blogPost != null)
            {
                BlogPost updatedBlogPost = mapper.Map<BlogPostDTO, BlogPost>(blogPostDto);
                foreach (TagDTO tagDTO in blogPostDto.TagDTOs)
                {
                    Tag tag;
                    if (tagDTO.Id == null)
                    {
                        tag = new Tag
                        {
                            Name = tagDTO.Name
                        };
                    }
                    else
                    {
                        tag = await DatabaseManager.Instance.Tag.FindAsync(tagDTO.Id);
                    }
                    BlogPostTag blogPostTag = await DatabaseManager.Instance.BlogPostTag.Where(bpt => bpt.BlogPostId == blogPost.Id && bpt.TagId == tag.Id).FirstOrDefaultAsync()
                        ?? new BlogPostTag { Tag = tag, BlogPost = updatedBlogPost };
                    updatedBlogPost.BlogPostTags.Add(blogPostTag);
                }
                PropertyInfo[] propertyInfos = blogPost.GetType().GetProperties();
                foreach (PropertyInfo property in propertyInfos)
                {
                    var val = property.GetValue(updatedBlogPost);
                    if (val != null)
                    {
                        if (!((property.Name == "UserId" || property.Name == "Id") && int.TryParse(val.ToString(), out int res) && res < 1) && property.Name != "Created")
                        {
                            property.SetValue(blogPost, val);
                        }
                    }
                }
                await DatabaseManager.Instance.SaveChangesAsync();
                return ConvertToBlogPostDTO(blogPost);
            }
            return null;
        }
        public static async Task<bool> Delete(int id)
        {
            try
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
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
        }
        public static async Task<int> GetCount()
        {
            return await DatabaseManager.Instance.BlogPost.CountAsync();
        }
        public static BlogPostDTO ConvertToBlogPostDTO(BlogPost blogPost)
        {
            BlogPostDTO blogPostDTO = mapper.Map<BlogPost, BlogPostDTO>(blogPost);
            if (blogPost != null)
            {
                List<TagDTO> tagDTOs = blogPost.BlogPostTags.Select(bpt => new TagDTO { Id = bpt.TagId, Name = bpt.Tag.Name, UserId = blogPost.UserId }).ToList();
                blogPostDTO.TagDTOs = tagDTOs;
            }
            return blogPostDTO;
        }
        private static BlogPost convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            return mapper.Map<BlogPostDTO, BlogPost>(blogPostDTO);
        }
    }
}
