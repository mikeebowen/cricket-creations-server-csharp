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
        private static MapperConfiguration config = new MapperConfiguration(c =>
        {
            c.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore());
        });
        private static IMapper _mapper = config.CreateMapper();

        public async Task<UserDTO> GetUser(int id)
        {
            User user = await DatabaseManager.Instance.User.Where(u => u.Id == id).FirstOrDefaultAsync();
            return _convertToUserDTO(user);
        }

        public async Task<UserDTO> CheckPassword(string userName, string password)
        {
            User user = await DatabaseManager.Instance.User.Where(u => u.Email == userName || u.UserName == userName).FirstOrDefaultAsync();

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

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(userDTO.Id);
            if (user != null)
            {
                User updatedUser = _convertToUser(userDTO);
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
                return _convertToUserDTO(user);
            }
            return null;
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

        public async Task<UserDTO> CheckRefreshToken(int id, string token)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(id);
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

        public async Task<bool> IsValidId(int id)
        {
            User user = await DatabaseManager.Instance.User.FindAsync(id);

            return user != null;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            List<User> existingEmail = await DatabaseManager.Instance.User.Where(u => u.Email == userDTO.Email).ToListAsync();
            List<User> existingUsername = await DatabaseManager.Instance.User.Where(u => u.UserName == userDTO.UserName).ToListAsync();

            if (existingEmail.Count() > 0)
            {
                throw new DbUpdateException($"{userDTO.Email} account already exists");
            }
            if (existingUsername.Count() > 0)
            {
                throw new DbUpdateException($"{userDTO.UserName} already exists");
            }
            User user = _convertToUser(userDTO);
            User newUser = DatabaseManager.Instance.User.Add(user).Entity;
            await DatabaseManager.Instance.SaveChangesAsync();
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
    }
}
