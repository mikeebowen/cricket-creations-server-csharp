using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using CricketCreations.Services;
using CricketCreations.Interfaces;

namespace CricketCreations.Services
{
    public enum Role
    {
        Administrator,
        User
    }
    public class UserService : IDataService<UserService>
    {
        [Key]
        public int Id { get; set; }
        public byte[] Salt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        public string Avatar { get; set; }
        public Role Role { get; set; }
        public List<BlogPostService> BlogPosts { get; set; } = new List<BlogPostService>();
        int? IDataService<UserService>.Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<UserRepository, UserService>()
        .ForMember(dest => dest.BlogPosts, opts => opts.Ignore())
        .ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<UserService>> GetAll(int? id)
        {
            List<UserRepository> userDTOs = await UserRepository.GetAll();
            return userDTOs.Select(userDTO => convertToUser(userDTO)).ToList();
        }
        public async Task<UserService> GetById(int id, bool? withPosts)
        {
            UserService user;
            if (withPosts != null && withPosts == true)
            {
                UserRepository userDTO = await UserRepository.GetUserDTOWithPosts(id);
                user = convertToUser(userDTO);
                foreach (BlogPostRepository blogPostDTO in userDTO.BlogPosts)
                {
                    //user.BlogPosts.Add(BlogPostService.ConvertToBlogPost(blogPostDTO));
                }
            }
            else
            {
                UserRepository userDTO = await UserRepository.GetUserDTO(id);
                user = convertToUser(userDTO);
            }
            return user;
        }
        private static UserService convertToUser(UserRepository userDTO)
        {
            return mapper.Map<UserRepository, UserService>(userDTO);
        }
        private static UserRepository convertToUserDTO(UserService user)
        {
            return mapper.Map<UserService, UserRepository>(user);
        }
        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserService>> GetRange(int page, int count, int? id)
        {
            throw new NotImplementedException();
        }

        public Task<UserService> Create(UserService t, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserService> Update(UserService user)
        {
            UserRepository userDto = convertToUserDTO(user);
            UserRepository updatedUserDto = await UserRepository.Update(userDto);
            UserService updatedUser = mapper.Map<UserService>(updatedUserDto);
            return updatedUser;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
        public static UserService CheckPassword(string password, string userEmail)
        {
            UserRepository userDTO = UserRepository.CheckPassword(password, userEmail);
            return convertToUser(userDTO);
        }
        public static async Task<UserService> CheckRefreshToken(int id, string token)
        {
            UserRepository userDTO = await UserRepository.CheckRefreshToken(id, token);
            if (userDTO != null)
            {
                return convertToUser(userDTO);
            }
            return null;
        }
    }
}
