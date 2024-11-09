using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;

namespace CricketCreations.Services
{
    public enum Role
    {
        Administrator,
        User,
    }

    public class UserService : IUserService
    {
        private static readonly MapperConfiguration _config = new MapperConfiguration(config =>
        {
            config.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Salt, options => options.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Token, options => options.Ignore());

            config.CreateMap<UserDTO, NewUser>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        });

        private static readonly IMapper _mapper = _config.CreateMapper();

        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

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

            return await _setTokens(userDTO);
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
            UserDTO newUserDTO = await _userRepository.Create(userDTO, newUser.Password);

            if (newUserDTO == null)
            {
                return null;
            }

            AuthenticationResponse authenticationResponse = await _generateTokens(newUserDTO);
            User user = _convertToUser(newUserDTO);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = newUserDTO.RefreshTokenExpiration;
            user.Token = authenticationResponse.Token;
            return user;
        }

        public async Task<bool> IsValidId(int id)
        {
            return await _userRepository.IsValidId(id);
        }

        public async Task<User> Update(NewUser user, int id)
        {
            UserDTO updatedUserDTO = _convertToUserDTO(user);
            updatedUserDTO.Id = id;

            UserDTO userDTO = await _userRepository.Update(updatedUserDTO);

            return _convertToUser(userDTO);
        }

        public async Task<bool> UpdatePassword(int userId, string password)
        {
            return await _userRepository.UpdatePassword(userId, password);
        }

        public async Task<bool> Logout(int id)
        {
            return await _userRepository.Logout(id);
        }

        public (bool IdBool, int IdInt) GetId(ClaimsPrincipal user)
        {
            List<Claim> claims = user.Claims.ToList();
            string idStr = claims?.FirstOrDefault(c => c.Type.Equals("Id", StringComparison.OrdinalIgnoreCase))?.Value;
            bool isInt = int.TryParse(idStr, out int id);

            return (isInt, id);
        }

        public async Task<bool> SetResetPasswordCode(string emailAddress)
        {
            return await _userRepository.SetResetPasswordCode(emailAddress);
        }

        public async Task<User> ValidateResetCode(int id, string resetCode)
        {
            UserDTO userDTO = await _userRepository.ValidateResetCode(id, resetCode);

            return await _setTokens(userDTO);
        }

        private static UserDTO _convertToUserDTO(NewUser newUser)
        {
            if (newUser == null)
            {
                return null;
            }

            UserDTO userDTO = _mapper.Map<UserDTO>(newUser);
            return userDTO;
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
                RefreshToken = refreshToken,
                Avatar = userDTO.Avatar,
            };
        }

        private async Task<User> _setTokens(UserDTO userDTO)
        {
            AuthenticationResponse authenticationResponse = await _generateTokens(userDTO);
            User user = _convertToUser(userDTO);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = userDTO.RefreshTokenExpiration;
            user.Token = authenticationResponse.Token;

            return user;
        }
    }
}
