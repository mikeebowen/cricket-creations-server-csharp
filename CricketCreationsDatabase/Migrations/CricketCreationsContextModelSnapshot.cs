﻿// <auto-generated />
using System;
using CricketCreationsDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CricketCreationsDatabase.Migrations
{
    [DbContext(typeof(CricketCreationsContext))]
    partial class CricketCreationsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.Property<int>("BlogPostsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("BlogPostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BlogPostTag");
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BlogPost");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
                            Created = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2106),
                            Deleted = false,
                            LastUpdated = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2117),
                            Published = true,
                            Title = "enim neque volutpat ac tincidunt",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
                            Created = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2730),
                            Deleted = false,
                            LastUpdated = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2739),
                            Published = true,
                            Title = "volutpat odio facilisis mauris sit",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
                            Created = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2742),
                            Deleted = false,
                            LastUpdated = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2744),
                            Published = true,
                            Title = "maecenas volutpat blandit aliquam etiam",
                            UserId = 1
                        },
                        new
                        {
                            Id = 4,
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.",
                            Created = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2746),
                            Deleted = false,
                            LastUpdated = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2748),
                            Published = true,
                            Title = "viverra mauris in aliquam sem",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Heading")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Page");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Bacon ipsum dolor amet strip steak bresaola chislic, bacon short loin kevin andouille brisket corned beef. Turducken spare ribs pork chop frankfurter, bresaola kielbasa meatball meatloaf pork chislic shoulder short loin leberkas. Frankfurter kevin bacon leberkas ham drumstick shankle flank t-bone biltong shank meatball pork chop bresaola turducken. Frankfurter bacon cupim, hamburger doner pork chop ribeye beef.",
                            Created = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2751),
                            Deleted = false,
                            Heading = "The About Page",
                            LastUpdated = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(2753),
                            Title = "About",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Content = "Fish tacos with cabbage slaw and a side of chips and guac. CARNITAS!! These tacos are lit 🔥. Can you put some peppers and onions on that? Black or pinto beans? Give me all the tacos, immediately. How bout a gosh darn quesadilla? Black or pinto beans? It’s taco time all the time. Um, Tabasco? No thanks, do you have any Cholula? It’s a wonderful morning for breakfast tacos. How do you feel about hard shelled tacos? Make it a double there pal. I’d have to say, those tacos are on fleek",
                            Created = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(3321),
                            Deleted = false,
                            Heading = "The Taco Page",
                            LastUpdated = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(3330),
                            Title = "Taco",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AvatarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2021, 7, 29, 10, 41, 1, 690, DateTimeKind.Local).AddTicks(128),
                            Deleted = false,
                            Email = "michael@example.com",
                            LastUpdated = new DateTime(2021, 7, 29, 10, 41, 1, 692, DateTimeKind.Local).AddTicks(122),
                            Name = "Michael",
                            Password = "zmutQuSwOByHU8zFy/Rv8O/P3Um0M13eM6F1004F1mc=",
                            RefreshTokenExpiration = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0,
                            Salt = new byte[] { 217, 113, 56, 2, 249, 126, 124, 52, 30, 93, 139, 128, 120, 78, 117, 128 },
                            Surname = "Test",
                            UserName = "tacocat"
                        });
                });

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.HasOne("CricketCreationsDatabase.Models.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("BlogPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CricketCreationsDatabase.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.BlogPost", b =>
                {
                    b.HasOne("CricketCreationsDatabase.Models.User", "User")
                        .WithMany("BlogPosts")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.Image", b =>
                {
                    b.HasOne("CricketCreationsDatabase.Models.User", "User")
                        .WithMany("Images")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.Page", b =>
                {
                    b.HasOne("CricketCreationsDatabase.Models.User", "User")
                        .WithMany("Pages")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.Tag", b =>
                {
                    b.HasOne("CricketCreationsDatabase.Models.User", "User")
                        .WithMany("Tags")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.User", b =>
                {
                    b.HasOne("CricketCreationsDatabase.Models.Image", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("CricketCreationsDatabase.Models.User", b =>
                {
                    b.Navigation("BlogPosts");

                    b.Navigation("Images");

                    b.Navigation("Pages");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
