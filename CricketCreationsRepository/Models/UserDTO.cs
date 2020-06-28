using AutoMapper;
using CricketCreationsDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static async Task<List<UserDTO>> GetAll()
        {
            List<User> users = await DatabaseManager.Instance.User.ToListAsync();
            return users.Select(u => convertToUserDTO(u)).ToList();
        }
        public static async Task<UserDTO> GetUserDTO(int id)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(id);
            return convertToUserDTO(user);
        }
        private static UserDTO convertToUserDTO(User user)
        {
            return mapper.Map<User, UserDTO>(user);
        }
    }
}
