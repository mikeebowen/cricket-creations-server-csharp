using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using CricketCreations.Models;
using CricketCreations.Interfaces;
using CricketCreationsRepository.Models;

namespace CricketCreations.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _expDate;

        public JwtService()
        {
            _secret = Environment.GetEnvironmentVariable("JWT_SECRET");
            _expDate = Environment.GetEnvironmentVariable("TOKEN_EXPIRATION_IN_MINUTES");
        }

        public string GenerateSecurityToken(UserDTO userDTO)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, userDTO.Email),
                    new Claim(ClaimTypes.Surname, userDTO.Surname),
                    new Claim(ClaimTypes.GivenName, userDTO.Name),
                    new Claim(ClaimTypes.NameIdentifier, userDTO.Id.ToString()),
                    new Claim("avatar", userDTO.Avatar ?? ""),
                    new Claim(ClaimTypes.Name, userDTO.UserName),
                    new Claim(ClaimTypes.Role, userDTO.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[255];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
