﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CricketCreationsDatabase.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<User> User { get; set; }

        public DbSet<BlogPost> BlogPost { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<Page> Page { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var editedEntities = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Modified).ToList();
            var addedEntities = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added).ToList();
            DateTime now = DateTime.Now;
            addedEntities.ForEach(entity =>
            {
                if (entity.Entity.GetType().GetProperty("Created") != null && entity.Entity.GetType().GetProperty("LastUpdated") != null)
                {
                    entity.Property("Created").CurrentValue = now;
                    entity.Property("LastUpdated").CurrentValue = now;
                }
            });

            editedEntities.ForEach(entity =>
            {
                if (entity.Entity.GetType().GetProperty("LastUpdated") != null)
                {
                    entity.Property("LastUpdated").CurrentValue = now;
                }
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().HasMany(u => u.BlogPosts);
            modelBuilder.Entity<User>().HasMany(u => u.Pages);
            modelBuilder.Entity<User>().HasMany(u => u.Tags);
            modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<int>();

            modelBuilder.Entity<BlogPost>().HasMany(b => b.Tags);
            modelBuilder.Entity<BlogPost>().HasOne(b => b.User).WithMany(u => u.BlogPosts);

            modelBuilder.Entity<Tag>().HasMany(t => t.BlogPosts);
            modelBuilder.Entity<Tag>().HasMany(t => t.Users);

            modelBuilder.Entity<Page>().HasOne(p => p.User).WithMany(u => u.Pages);
            modelBuilder.Entity<Page>().HasIndex(p => p.Heading).IsUnique();

            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            optionsBuilder.UseSqlServer(connectionString);

            // optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CricketCreations_Dev;Trusted_Connection=True;");
        }
    }
}

#pragma warning disable SA1402 // File may only contain a single type
public static class ModelBuilderExtensions
#pragma warning restore SA1402 // File may only contain a single type
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string password =
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: "password",
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Name = "Michael",
                Surname = "Test",
                Email = "michael@example.com",
                UserName = "tacocat",
                Role = Role.Administrator,
                Password = password,
                Salt = salt,
                BlogPosts = new List<BlogPost>(),
                Tags = new List<Tag>(),
                Pages = new List<Page>(),
            });

        // modelBuilder.Entity<BlogPost>().HasData(
        //    new BlogPost
        //    {
        //        Published = true,
        //        Id = 1,
        //        Created = DateTime.Now,
        //        LastUpdated = DateTime.Now,
        //        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
        //        Title = "enim neque volutpat ac tincidunt",
        //        UserId = 1
        //    },
        //     new BlogPost
        //     {
        //         Published = true,
        //         Id = 2,
        //         Created = DateTime.Now,
        //         LastUpdated = DateTime.Now,
        //         Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
        //         Title = "volutpat odio facilisis mauris sit",
        //         UserId = 1
        //     },
        //      new BlogPost
        //      {
        //          Published = true,
        //          Id = 3,
        //          Created = DateTime.Now,
        //          LastUpdated = DateTime.Now,
        //          Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
        //          Title = "maecenas volutpat blandit aliquam etiam",
        //          UserId = 1
        //      },
        //      new BlogPost
        //      {
        //          Published = true,
        //          Id = 4,
        //          Created = DateTime.Now,
        //          LastUpdated = DateTime.Now,
        //          Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
        //          Title = "viverra mauris in aliquam sem",
        //          UserId = 1
        //      }
        // );
        // modelBuilder.Entity<Page>().HasData(
        //    new Page
        //    {
        //        // UserId = 1,
        //        Id = 1,
        //        Title = "About",
        //        Heading = "The About Page",
        //        Content = "Bacon ipsum dolor amet strip steak bresaola chislic, bacon short loin kevin andouille brisket corned beef. Turducken spare ribs pork chop frankfurter, bresaola kielbasa meatball meatloaf pork chislic shoulder short loin leberkas. Frankfurter kevin bacon leberkas ham drumstick shankle flank t-bone biltong shank meatball pork chop bresaola turducken. Frankfurter bacon cupim, hamburger doner pork chop ribeye beef.",
        //        Created = DateTime.Now,
        //        LastUpdated = DateTime.Now
        //    },
        //    new Page
        //    {
        //        // UserId = 1,
        //        Id = 2,
        //        Title = "Taco",
        //        Heading = "The Taco Page",
        //        Content = "Fish tacos with cabbage slaw and a side of chips and guac. CARNITAS!! These tacos are lit 🔥. Can you put some peppers and onions on that? Black or pinto beans? Give me all the tacos, immediately. How bout a gosh darn quesadilla? Black or pinto beans? It’s taco time all the time. Um, Tabasco? No thanks, do you have any Cholula? It’s a wonderful morning for breakfast tacos. How do you feel about hard shelled tacos? Make it a double there pal. I’d have to say, those tacos are on fleek",
        //        Created = DateTime.Now,
        //        LastUpdated = DateTime.Now
        //    }
        //    );
        // modelBuilder.Entity<Tag>().HasData(
        //    new Tag { Id = 1, Name = "car" }
        // );
        // modelBuilder.Entity<BlogPostTag>().HasData(
        //    new BlogPostTag { Id = 1, BlogPostId = 1, TagId = 1 }
        // );
    }
}
