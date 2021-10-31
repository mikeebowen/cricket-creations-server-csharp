using CricketCreations.Models;
using CricketCreationsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IJwtService
    {
        public abstract string GenerateSecurityToken(UserDTO userDTO);
        public abstract string GenerateRefreshToken();
    }
}
