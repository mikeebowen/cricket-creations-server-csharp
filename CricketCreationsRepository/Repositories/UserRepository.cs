using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using AutoMapper;
using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CricketCreationsRepository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly MapperConfiguration _config = new MapperConfiguration(c =>
        {
            c.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        });

        private static readonly IMapper _mapper = _config.CreateMapper();

        private readonly IDatabaseManager _databaseManager;

        public UserRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
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

        public async Task<UserDTO> GetUser(int id)
        {
            User user = await _databaseManager.Instance.User.Where(u => u.Id == id).FirstOrDefaultAsync();
            return _convertToUserDTO(user);
        }

        public async Task<bool> UpdatePassword(int userId, string password)
        {
            User user = await _databaseManager.Instance.FindAsync<User>(userId);

            if (user != null)
            {
                user.Salt = _getSalt();
                user.Password = HashPassword(password, user.Salt);

                await _databaseManager.Instance.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
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
                User updatedUser = _convertToUser(userDTO);
                PropertyInfo[] propertyInfos = user.GetType().GetProperties();
                foreach (PropertyInfo property in propertyInfos)
                {
                    var val = property.GetValue(updatedUser);
                    if (val != null)
                    {
                        if (
                                !(property.Name != "Id" && int.TryParse(val.ToString(), out int res) && res < 1) &&
                                property.Name != "Created" &&
                                property.Name != "LastUpdated")
                        {
                            property.SetValue(user, val);
                        }
                    }
                }

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

        public async Task<UserDTO> Create(UserDTO userDTO, string password)
        {
            User user = _convertToUser(userDTO);
            user.BlogPosts = new List<BlogPost>();
            user.Tags = new List<Tag>();
            user.Salt = _getSalt();
            user.Password = HashPassword(password, user.Salt);

            User newUser = _databaseManager.Instance.User.Add(user).Entity;

            await _databaseManager.Instance.SaveChangesAsync();

            return _convertToUserDTO(newUser);
        }

        public async Task<bool> Logout(int id)
        {
            User user = await _databaseManager.Instance.User.FindAsync(id);
            user.RefreshToken = null;

            await _databaseManager.Instance.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetResetPasswordCode(string toEmail)
        {
            if (toEmail == null || new EmailAddressAttribute().IsValid(toEmail) == false)
            {
                return false;
            }

            Random rnd = new Random();

            string resetCode = string.Empty;

            for (int i = 0; i < 6; i++)
            {
                resetCode = string.Concat(resetCode, rnd.Next(10).ToString());
            }

            string fromEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");

            MailMessage mailMessage = new MailMessage(fromEmail, toEmail);

            mailMessage.Subject = "mikeebowen.com password reset";
            mailMessage.Body = string.Concat("<h1>Use this code to reset your password for mikeebowen.com</h1><h2>This code will expire in 1 hour</h2><p><b>", resetCode, "</p><br><p>Follow this link to enter the code and reset your password</p><p>https://mikeebowen.com</p><br><p>If you did not request to reset your password, please ignore this email</p>");
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            bool hasPort = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out int port);

            if (!hasPort)
            {
                return false;
            }

            User user = _databaseManager.Instance.User.Where(u => u.Email == toEmail).FirstOrDefault();

            if (user != null)
            {
                user.ResetCode = resetCode;
                user.ResetCodeExpiration = DateTime.Now.AddHours(1);
                await _databaseManager.Instance.SaveChangesAsync();

                SmtpClient smtpClient = new SmtpClient(Environment.GetEnvironmentVariable("SMTP_SERVER"), port);
                NetworkCredential networkCredential = new NetworkCredential(Environment.GetEnvironmentVariable("SMTP_USER"), Environment.GetEnvironmentVariable("SMTP_PW"));
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;

                smtpClient.Send(mailMessage);

                return true;
            }
            else
            {
                return false;
            }
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

        private static byte[] _getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }
    }
}
