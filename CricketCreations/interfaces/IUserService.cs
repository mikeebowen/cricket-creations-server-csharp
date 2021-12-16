using System.Threading.Tasks;
using CricketCreations.Models;

namespace CricketCreations.Interfaces
{
    public interface IUserService
    {
        public abstract Task<User> GetUser(int id);

        public abstract Task<User> Create(NewUser newUser);

        public abstract Task<User> Update(NewUser user, int id);

        public abstract Task<User> CheckPassword(string userName, string password);

        public abstract Task<bool> UpdatePassword(int userId, string password);

        public abstract Task<AuthenticationResponse> CheckRefreshToken(int id, string refreshToken);

        public abstract Task<bool> IsValidId(int id);

        public abstract Task<bool> Logout(int id);
    }
}
