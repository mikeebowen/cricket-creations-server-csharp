using CricketCreationsDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CricketCreationsDatabase
{
    public class CricketCreationsContext : DbContext
    {
        public CricketCreationsContext()
        {

        }
        public CricketCreationsContext(DbContextOptions<CricketCreationsContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CricketCreations_Dev;Trusted_Connection=True;");
        }
        public DbSet<User> User { get; set; }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<BlogPostTag> BlogPostTag { get; set; }
        public DbSet<PageContent> PageContent { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.BlogPosts);
            modelBuilder.Entity<BlogPostTag>()
                .HasOne(b => b.BlogPost)
                .WithMany(b => b.BlogPostTags)
                .HasForeignKey(bc => bc.BlogPostId);
            modelBuilder.Entity<BlogPostTag>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.BlogPostTags)
                .HasForeignKey(bc => bc.TagId);
            modelBuilder.Entity<BlogPostTag>().HasKey(k => new { k.BlogPostId, k.TagId });
            modelBuilder.Seed();
        }
    }
}
public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "Michael",
                SurName = "Test",
                Email = "michael@example.com"
            }
        );
        modelBuilder.Entity<BlogPost>().HasData(
            new BlogPost
            {
                UserId = 1,
                Id = 1,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
                Title = "enim neque volutpat ac tincidunt"
            },
             new BlogPost
             {
                 UserId = 1,
                 Id = 2,
                 Created = DateTime.Now,
                 LastUpdated = DateTime.Now,
                 Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
                 Title = "volutpat odio facilisis mauris sit"
             },
              new BlogPost
              {
                  UserId = 1,
                  Id = 3,
                  Created = DateTime.Now,
                  LastUpdated = DateTime.Now,
                  Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
                  Title = "maecenas volutpat blandit aliquam etiam"
              },
              new BlogPost
              {
                  UserId = 1,
                  Id = 4,
                  Created = DateTime.Now,
                  LastUpdated = DateTime.Now,
                  Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
                  Title = "viverra mauris in aliquam sem"
              }
        );
        //modelBuilder.Entity<Tag>().HasData(
        //    new Tag { Id = 1, Name = "car" }
        //);
        //modelBuilder.Entity<BlogPostTag>().HasData(
        //    new BlogPostTag { Id = 1, BlogPostId = 1, TagId = 1 }
        //);
    }
}