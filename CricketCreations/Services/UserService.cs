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
            config.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Salt, options => options.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Token, options => options.Ignore()
            );
            config.CreateMap<UserDTO, NewUser>().ReverseMap();
        });
        private static IMapper _mapper = _config.CreateMapper();
        public async Task<User> GetUser(int id)
        {
            UserDTO userDTO = await _userRepository.GetUser(id);
            return _convertToUser(userDTO);
        }

        public async Task<User> CheckPassword(string userName, string password)
        {
            UserDTO userDTO = await _userRepository.CheckPassword(userName, password);
            if (userDTO == null)
            {
                return null;
            }
            AuthenticationResponse authenticationResponse = await _generateTokens(userDTO);
            User user = _convertToUser(userDTO);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = userDTO.RefreshTokenExpiration;
            user.Token = authenticationResponse.Token;
            return user;
        }

        public async Task<AuthenticationResponse> CheckRefreshToken(int id, string refreshToken)
        {
            UserDTO userDTO = await _userRepository.CheckRefreshToken(id, refreshToken);

            if (userDTO == null)
            {
                return null;
            }
            return await _generateTokens(userDTO);
        }

        public async Task<User> Create(NewUser newUser)
        {
            UserDTO userDTO = _convertToUserDTO(newUser);
            UserDTO newUserDTO = await _userRepository.Create(userDTO);

            if (newUserDTO == null)
            {
                return null;
            }

            UserDTO updatedUserDTO = await _userRepository.Update(newUserDTO);
            AuthenticationResponse authenticationResponse = await _generateTokens(updatedUserDTO);
            User user = _convertToUser(updatedUserDTO);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = updatedUserDTO.RefreshTokenExpiration;
            user.Token = authenticationResponse.Token;
            return user;
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

        private static UserDTO _convertToUserDTO(NewUser newUser)
        {
            if (newUser == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(newUser);
        }

        private static User _convertToUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return null;
            }
            return _mapper.Map<User>(userDTO);
        }
        private async Task<AuthenticationResponse> _generateTokens(UserDTO userDTO)
        {
            string token = _jwtService.GenerateSecurityToken(userDTO);
            string refreshToken = _jwtService.GenerateRefreshToken();
            userDTO.RefreshToken = refreshToken;
            userDTO.RefreshTokenExpiration = DateTime.Now.AddDays(7);
            await _userRepository.Update(userDTO);

            return new AuthenticationResponse()
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<User> Update(User user)
        {
            UserDTO updatedUserDTO = _convertToUserDTO(user);

            UserDTO userDTO = await _userRepository.Update(updatedUserDTO);

            return _convertToUser(userDTO);
        }
    }
}
