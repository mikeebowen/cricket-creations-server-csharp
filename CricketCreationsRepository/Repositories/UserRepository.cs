using AutoMapper;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
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

namespace CricketCreationsRepository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDatabaseManager _databaseManager;

        public UserRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        });
        private static IMapper _mapper = config.CreateMapper();

        public async Task<UserDTO> GetUser(int id)
        {
            User user = await _databaseManager.Instance.User.Where(u => u.Id == id).FirstOrDefaultAsync();
            return _convertToUserDTO(user);
        }

        public async Task<UserDTO> CheckPassword(string userName, string password)
        {
            User user = await _databaseManager.Instance.User.Where(u => u.Email == userName || u.UserName == userName).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }
            if (HashPassword(password, user.Salt) == user.Password)
            {
                return _convertToUserDTO(user);
            }
            return null;
        }

        public async Task<UserDTO> CheckRefreshToken(int id, string token)
        {
            User user = await _databaseManager.Instance.User.FindAsync(id);
            int dateDiff = DateTime.Compare(user.RefreshTokenExpiration, DateTime.Now);

            if (user != null && HashPassword(token, user.Salt) == user.RefreshToken && dateDiff > 0)
            {
                return _convertToUserDTO(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            User user = await _databaseManager.Instance.User.FindAsync(userDTO.Id);
            if (user != null)
            {
                user.Password = userDTO.Password ?? user.Password;
                user.Salt = userDTO.Salt ?? user.Salt;
                user.RefreshToken = userDTO.RefreshToken ?? user.RefreshToken;
                if (userDTO.RefreshToken != null)
                {
                user.RefreshTokenExpiration = userDTO.RefreshTokenExpiration;
                }
                user.Name = userDTO.Name ?? user.Name;
                user.Surname = userDTO.Surname ?? user.Surname;
                user.Email = userDTO.Email ?? user.Email;
                user.UserName = userDTO.UserName ?? user.UserName;
                user.Role = (Role)userDTO.Role;
                user.Avatar = userDTO.Avatar ?? user.Avatar;


                await _databaseManager.Instance.SaveChangesAsync();
                return _convertToUserDTO(user);
            }
            return null;
        }

        public async Task<bool> IsValidId(int id)
        {
            User user = await _databaseManager.Instance.User.FindAsync(id);

            return user != null;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            User user = _convertToUser(userDTO);
            user.BlogPosts = new List<BlogPost>();
            user.Tags = new List<Tag>();

            User newUser = _databaseManager.Instance.User.Add(user).Entity;

            await _databaseManager.Instance.SaveChangesAsync();

            return _convertToUserDTO(newUser);
        }

        private static User _convertToUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return null;
            }
            return _mapper.Map<User>(userDTO);
        }
        private static UserDTO _convertToUserDTO(User user)
        {
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(user);
        }
        public static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
