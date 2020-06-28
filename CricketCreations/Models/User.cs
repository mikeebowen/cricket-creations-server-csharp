using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricketCreations.Models
{
    public class User
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
        public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<ActionResult<Task<IEnumerable<User>>>> GetAll()
        {
            List<UserDTO> userDTOs = await UserDTO.GetAll();
            return Task.FromResult(userDTOs.Select(userDTO => convertToUser(userDTO)));
        }
        public static async Task<User> GetUser(int id)
        {
            UserDTO userDTO = await UserDTO.GetUserDTO(id);
            return convertToUser(userDTO);
        }
        private static User convertToUser(UserDTO userDTO)
        {
            return mapper.Map<UserDTO, User>(userDTO);
        }
    }
}
