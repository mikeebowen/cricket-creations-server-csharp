using AutoMapper;
using CricketCreationsDatabase.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Models
{
    public enum Role
    {
        Administrator,
        User
    }
    public class UserDTO
    {
        private string password;
        private string refreshToken;

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
                return password;
            }
            set
            {
                Salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(Salt);
                }
                password = hashString(value, Salt);
            }
        }
        [Required]
        public byte[] Salt { get; set; }
        public string RefreshToken
        {
            get
            {
                return refreshToken;
            }
            set
            {
                refreshToken = hashString(value ?? "", Salt);
            }
        }
        public DateTime RefreshTokenExpiration { get; set; }
        [Required, MaxLength(200)]
        public string Email { get; set; }
        [Required, MaxLength(200)]
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public Role Role { get; set; }
        public List<BlogPostDTO> BlogPosts { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>()
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
            .ReverseMap());
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
        public static async Task<UserDTO> Update(UserDTO userDto)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(userDto.Id);
            if (user != null)
            {
                User updatedUser = mapper.Map<User>(userDto);
                PropertyInfo[] propertyInfos = user.GetType().GetProperties();
                foreach (PropertyInfo property in propertyInfos)
                {
                    var val = property.GetValue(updatedUser);
                    if (val != null)
                    {
                        if (!(property.Name != "Id" && int.TryParse(val.ToString(), out int res) && res < 1) && property.Name != "Created" && property.Name != "Salt")
                        {
                            property.SetValue(user, val);
                        }
                    }
                }
                await DatabaseManager.Instance.SaveChangesAsync();
                return mapper.Map<UserDTO>(user);
            }
            return null;
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
        public static UserDTO CheckPassword(string password, string userName)
        {
            var user = DatabaseManager.Instance.User.Where(u => u.Email == userName).First();
            if (user == null)
            {
                return null;
            }
            if (hashString(password, user.Salt) == user.Password)
            {
                return ConvertToUserDTO(user);
            }
            else
            {
                return null;
            }
        }
        public static async Task<UserDTO> CheckRefreshToken(int id, string token)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(id);
            int dateDiff = DateTime.Compare(user.RefreshTokenExpiration, DateTime.Now);
            if (user.RefreshToken == hashString(token, user.Salt) && dateDiff > 0)
            {
                return ConvertToUserDTO(user);
            }
            return null;
        }
        private static string hashString(string pw, byte[] salt)
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
