﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Models;
using Microsoft.AspNetCore.Mvc;
using CricketCreations.Models;
using CricketCreations.Interfaces;

namespace CricketCreations.Models
{
    public enum Role
    {
        Administrator,
        User
    }
    public class User: IDataModel<User>
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
        public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        int? IDataModel<User>.Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<UserDTO, User>()
        .ForMember(dest => dest.BlogPosts, opts => opts.Ignore())
        .ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public async Task<List<User>> GetAll(int? id)
        {
            List<UserDTO> userDTOs = await UserDTO.GetAll();
            return userDTOs.Select(userDTO => convertToUser(userDTO)).ToList();
        }
        public async Task<User> GetById(int id, bool? withPosts)
        {
            User user;
            if (withPosts != null && withPosts == true)
            {
                UserDTO userDTO = await UserDTO.GetUserDTOWithPosts(id);
                user = convertToUser(userDTO);
                foreach (BlogPostDTO blogPostDTO in userDTO.BlogPosts)
                {
                    user.BlogPosts.Add(BlogPost.ConvertToBlogPost(blogPostDTO));
                }
            }
            else
            {
                UserDTO userDTO = await UserDTO.GetUserDTO(id);
                user = convertToUser(userDTO);
            }
            return user;
        }
        private static User convertToUser(UserDTO userDTO)
        {
            return mapper.Map<UserDTO, User>(userDTO);
        }
        private static UserDTO convertToUserDTO(User user)
        {
            return mapper.Map<User, UserDTO>(user);
        }
        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetRange(int page, int count, int? id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Create(User t)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(User user)
        {
            UserDTO userDto = convertToUserDTO(user);
            UserDTO updatedUserDto = await UserDTO.Update(userDto);
            User updatedUser = mapper.Map<User>(updatedUserDto);
            return updatedUser;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
        public static User CheckPassword(string password, string userEmail)
        {
            UserDTO userDTO = UserDTO.CheckPassword(password, userEmail);
            return convertToUser(userDTO);
        }
        public static async Task<User> CheckRefreshToken(int id, string token)
        {
            UserDTO userDTO = await UserDTO.CheckRefreshToken(id, token);
            if (userDTO != null)
            {
                return convertToUser(userDTO);
            }
            return null;
        }
    }
}
