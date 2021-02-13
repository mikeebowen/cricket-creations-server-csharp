using AutoMapper;
using CricketCreationsDatabase.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Models
{
    public class UserDTO
    {
        private string password;

        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; } = false;
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                Salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(Salt);
                }
                password = hashPassword(value, Salt);
            }
        }
        [Required]
        public byte[] Salt { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public List<BlogPostDTO> BlogPosts { get; set; } = new List<BlogPostDTO>();
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>()
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore()).ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static async Task<List<UserDTO>> GetAll()
        {
            List<User> users = await DatabaseManager.Instance.User.ToListAsync();
            return users.Select(u => ConvertToUserDTO(u)).ToList();
        }
        public static async Task<UserDTO> GetUserDTOWithPosts(int id)
        {
            User user = await DatabaseManager.Instance.User.Include(user => user.BlogPosts).Where(user => user.Id == id).FirstAsync();
            UserDTO userDTO = ConvertToUserDTO(user);
            foreach (BlogPost blogPost in user.BlogPosts)
            {
                userDTO.BlogPosts.Add(BlogPostDTO.ConvertToBlogPostDTO(blogPost));
            }
            return userDTO;
        }
        public static async Task<UserDTO> GetUserDTO(int id)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(id);
            return ConvertToUserDTO(user);
        }
        public static UserDTO ConvertToUserDTO(User user)
        {
            return mapper.Map<User, UserDTO>(user);
        }
        public static bool CheckPassword(string password, string userName)
        {
            var user = DatabaseManager.Instance.User.Where(u => u.Email == userName).First();
            if (user == null)
            {
                return false;
            }
            return hashPassword(password, user.Salt) == user.Password;
        }
        private static string hashPassword(string pw, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: pw,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
