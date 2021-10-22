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
    }
}
