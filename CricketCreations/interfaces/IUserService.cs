using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        public abstract (bool IdBool, int IdInt) GetId(ClaimsPrincipal user);

        public abstract Task<bool> SetResetPasswordCode(string emailAddress);

        public abstract Task<User> ValidateResetCode(int id, string resetCode);
    }
}
