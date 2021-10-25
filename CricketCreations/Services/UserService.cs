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
            config.CreateMap<UserDTO, NewUser>().ReverseMap();
        });
        private static IMapper _mapper = _config.CreateMapper();
        public async Task<User> GetUser(int id)
        {
            UserDTO userDTO = await _userRepository.GetUser(id);
            return _convertToUser(userDTO);
        }

        public async Task<AuthenticationResponse> CheckPassword(string userName, string password)
        {
            UserDTO userDTO = await _userRepository.CheckPassword(userName, password);
            if (userDTO == null)
            {
                return null;
            }
            User user = _convertToUser(userDTO);
            string token = _jwtService.GenerateSecurityToken(user);
            string refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddDays(7);
            UserDTO updatedUserDTO =_convertToUserDTO(user);
            await _userRepository.Update(updatedUserDTO);

            return new AuthenticationResponse()
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthenticationResponse> CheckRefreshToken(int id, string refreshToken)
        {
            UserDTO userDTO = await _userRepository.CheckRefreshToken(id, refreshToken);

            if (userDTO == null)
            {
                return null;
            }

            User user = _convertToUser(userDTO);

            string token = _jwtService.GenerateSecurityToken(user);
            string newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddDays(7);
            UserDTO userDTO1 =_convertToUserDTO(user);
            await _userRepository.Update(userDTO1);

            return new AuthenticationResponse()
            {
                Token = token,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<User> Create(NewUser newUser)
        {
            UserDTO userDTO = _convertToUserDTO(newUser);
            UserDTO newUserDTO = await _userRepository.Create(userDTO);
            return _convertToUser(newUserDTO);
        }

        public async Task<bool> IsValidId(int id)
        {
            return await _userRepository.IsValidId(id);
        }

        private UserDTO _convertToUserDTO(User user)
        {
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(user);
        }

        private UserDTO _convertToUserDTO(NewUser newUser)
        {
            if (newUser == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(newUser);
        }

        private User _convertToUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return null;
            }
            return _mapper.Map<User>(userDTO);
        }
    }
}
