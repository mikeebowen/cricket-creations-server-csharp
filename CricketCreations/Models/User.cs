using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CricketCreations.Services;

namespace CricketCreations.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

        public string Token { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public string Surname { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(200)]
        public string UserName { get; set; }

        [IsValidBase64]
        public string Avatar { get; set; }

        public Role Role { get; set; }

        public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public string ResetCode { get; set; }

        public DateTime ResetCodeExpiration { get; set; }
    }
}
