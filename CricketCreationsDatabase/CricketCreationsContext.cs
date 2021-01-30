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
        public DbSet<Page> Page { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.BlogPosts);
            modelBuilder.Entity<User>().HasMany(u => u.Pages);
            modelBuilder.Entity<User>().HasMany(u => u.Tags);
            modelBuilder.Entity<BlogPostTag>()
                .HasOne(b => b.BlogPost)
                .WithMany(b => b.BlogPostTags)
                .HasForeignKey(bc => bc.BlogPostId);
            modelBuilder.Entity<BlogPostTag>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.BlogPostTags)
                .HasForeignKey(bc => bc.TagId);
            modelBuilder.Entity<BlogPostTag>().HasKey(k => new { k.BlogPostId, k.TagId });
            modelBuilder.Entity<BlogPostTag>().HasAlternateKey(e => e.Id);
            modelBuilder.Entity<Tag>().HasOne(t => t.User);
            modelBuilder.Entity<Page>().HasOne(p => p.User);
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
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
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
        modelBuilder.Entity<Page>().HasData(
            new Page
            {
                UserId = 1,
                Id = 1,
                Title = "About",
                Heading = "The About Page",
                Content = "Bacon ipsum dolor amet strip steak bresaola chislic, bacon short loin kevin andouille brisket corned beef. Turducken spare ribs pork chop frankfurter, bresaola kielbasa meatball meatloaf pork chislic shoulder short loin leberkas. Frankfurter kevin bacon leberkas ham drumstick shankle flank t-bone biltong shank meatball pork chop bresaola turducken. Frankfurter bacon cupim, hamburger doner pork chop ribeye beef.",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            },
            new Page
            {
                UserId = 1,
                Id = 2,
                Title = "Taco",
                Heading = "The Taco Page",
                Content = "Fish tacos with cabbage slaw and a side of chips and guac. CARNITAS!! These tacos are lit 🔥. Can you put some peppers and onions on that? Black or pinto beans? Give me all the tacos, immediately. How bout a gosh darn quesadilla? Black or pinto beans? It’s taco time all the time. Um, Tabasco? No thanks, do you have any Cholula? It’s a wonderful morning for breakfast tacos. How do you feel about hard shelled tacos? Make it a double there pal. I’d have to say, those tacos are on fleek",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
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