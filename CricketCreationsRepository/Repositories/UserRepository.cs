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
        private static IMapper mapper = config.CreateMapper();

        public async Task<UserDTO> GetUser(int id)
        {
            User user = await DatabaseManager.Instance.User.Where(u => u.Id == id).FirstOrDefaultAsync();
            return mapper.Map<UserDTO>(user);
        }
    }
}
