using CRM.Models;

namespace CRM.Services.Interfaces
{
    public interface IUserService
    {
        bool DeleteUser(string username);
        bool RestoreUser(string username);
        public bool CheckUsernameExistence(string username);
        public Task<AppUser> ReturnUserForLogin(string username);
    }
}
