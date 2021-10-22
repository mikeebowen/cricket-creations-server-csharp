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
using CricketCreationsRepository.Models;
using CricketCreations.Models;
using CricketCreationsRepository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CricketCreations.Services
{
    public enum Role
    {
        Administrator,
        User
    }
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        private static MapperConfiguration _config = new MapperConfiguration(config =>
        {
            config.CreateMap<User, UserDTO>().ReverseMap();
        });
        private static IMapper _mapper = _config.CreateMapper();
        public async Task<User> GetUser(int id)
        {
            UserDTO userDTO = await _userRepository.GetUser(id);
            return _mapper.Map<User>(userDTO);
        }
    }
}
