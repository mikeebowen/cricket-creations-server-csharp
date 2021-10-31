using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using CricketCreationsRepository.Repositories;

namespace CricketCreationsRepository.Models
{
    public class UserDTO
    {
        private string _password;
        private string _refreshToken;

        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; } = false;
        [Required, MaxLength(200)]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required, MaxLength(200), EmailAddress]
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                Salt = _getSalt();
                _password = UserRepository.HashPassword(value, Salt);
            }
        }
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
        [Required, MaxLength(200)]
        public string Email { get; set; }
        [Required, MaxLength(200)]
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public RoleTypes Role { get; set; }
        public List<BlogPostDTO> BlogPosts { get; set; }
        public List<TagDTO> Tags { get; set; }

        private static byte[] _getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return bytes;
        }

        public enum RoleTypes
        {
            Administrator,
            User
        }
    }
}
