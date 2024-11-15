﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CricketCreationsRepository.Repositories;
using Ganss.Xss;

namespace CricketCreationsRepository.Models
{
    public class UserDTO
    {
        private HtmlSanitizer _htmlSanitizer = new HtmlSanitizer();
        private string _name;
        private string _surname;
        private string _userName;
        private string _refreshToken;

        public enum RoleTypes
        {
            Administrator,
            User,
        }

        [Key]
        public int Id { get; set; }

        public bool Deleted { get; set; } = false;

        [Required]
        [MaxLength(200)]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = _htmlSanitizer.Sanitize(value);
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }

            set
            {
                _surname = _htmlSanitizer.Sanitize(value);
            }
        }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        [Required]
        public byte[] Salt { get; set; }

        public string RefreshToken
        {
            get
            {
                return _refreshToken;
            }

            set
            {
                if (value != null)
                {
                    _refreshToken = UserRepository.HashPassword(value, Salt);
                }
            }
        }

        public DateTime RefreshTokenExpiration { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = _htmlSanitizer.Sanitize(value);
            }
        }

        public string Avatar { get; set; }

        public RoleTypes Role { get; set; }

        public List<BlogPostDTO> BlogPosts { get; set; }

        public List<TagDTO> Tags { get; set; }

        public string ResetCode { get; set; }

        public DateTime ResetCodeExpiration { get; set; }
    }
}
