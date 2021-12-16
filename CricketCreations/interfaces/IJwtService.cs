using CricketCreationsRepository.Models;

namespace CricketCreations.Interfaces
{
    public interface IJwtService
    {
        public abstract string GenerateSecurityToken(UserDTO userDTO);

        public abstract string GenerateRefreshToken();
    }
}
