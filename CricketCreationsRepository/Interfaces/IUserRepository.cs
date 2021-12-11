using CricketCreationsDatabase.Models;
using CricketCreationsRepository.Models;
using CricketCreationsRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface IUserRepository
    {
        public abstract Task<UserDTO> GetUser(int id);
        public abstract Task<UserDTO> CheckPassword(string userName, string password);
        public abstract Task<bool> UpdatePassword(int userId, string password);
        public abstract Task<UserDTO> Create(UserDTO userDTO, string password);
        public abstract Task<UserDTO> Update(UserDTO userDTO);
        public abstract Task<UserDTO> CheckRefreshToken(int id, string token);
        public abstract Task<bool> IsValidId(int id);
        public abstract Task<bool> Logout(int id);
    }
}
