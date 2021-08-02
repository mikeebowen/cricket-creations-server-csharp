﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreationsDatabase.Models
{
    public enum Role
    {
        Administrator,
        User
    }
    public class User : BaseEntity
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public bool Deleted { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Surname { get; set; }
        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(200)]
        public string UserName { get; set; }
        [DefaultValue(Role.User)]
        public Role Role { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Page> Pages { get; set; }
        public List<Image> Images { get; set; }
        public Image Avatar { get; set; }

        public User()
        {
            this.Deleted = false;
            this.BlogPosts = new List<BlogPost>();
            this.Tags = new List<Tag>();
            this.Pages = new List<Page>();
            this.Images = new List<Image>();
        }
    }
}
