using CricketCreations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IJwtService
    {
        public abstract string GenerateSecurityToken(User user);
        public abstract string GenerateRefreshToken();
    }
}
