using CRM.DAL;
using CRM.Models;
using CRM.Repositories.Interfaces;
using CRM.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public bool DeleteUser(string username)
        {
            var user = _userRepository.DeleteUser(username);
            if (user == null)
                return false;
            user.IsDeleted = true;
                if (user.Accounts != null)
                {
                  user.Accounts.ForEach(a => a.IsDeleted = true);
                }
            _userRepository.Commit();
            return true;
        }

        public bool RestoreUser(string username)
        {
            var user = _userRepository.RestoreUser(username);
            if (user is null)
                return false;
            user.IsDeleted = false;
            if (user.Accounts != null)
            {
                user.Accounts.ForEach(a => a.IsDeleted = false);
            }
            _userRepository.Commit();
            return true;
        }
        #region Some Methods for Authorization Controller
        public bool CheckUsernameExistence(string username)
        {
            return _userManager.Users.Any(x => x.UserName == username);
        }

        public async Task<AppUser> ReturnUserForLogin(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username && x.IsDeleted == false);
        }
        #endregion
    }
}
