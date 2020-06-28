using AutoMapper;
using CricketCreationsDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CricketCreationsRepository.Models
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string SurName { get; set; }
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }
        public string Avatar { get; set; }
        public List<BlogPostDTO> BlogPosts { get; set; } = new List<BlogPostDTO>();
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static List<UserDTO> GetAll()
        {
            return DatabaseManager.Instance.User.Select(u => mapper.Map<User, UserDTO>(u)).ToList();
        }
    }
}
