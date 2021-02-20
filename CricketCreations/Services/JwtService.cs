using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using CricketCreations.Models;
using CricketCreationsRepository.Models;

namespace CricketCreations.Services
{
    public class JwtService
    {
        private readonly string secret;
        private readonly string expDate;

        public JwtService(IConfiguration config)
        {
            secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        public string GenerateSecurityToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim(ClaimTypes.GivenName, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Avatar", user.Avatar ?? ""),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
