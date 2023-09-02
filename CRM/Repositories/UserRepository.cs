using CRM.Models;
using CRM.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public AppUser DeleteUser(string username)
        {
            return  _userManager.Users.Include(x => x.Accounts).FirstOrDefault(x => x.UserName == username && x.IsDeleted == false);
        }

        public AppUser RestoreUser(string username)
        {
            return _userManager.Users.Include(x => x.Accounts).FirstOrDefault(x => x.UserName == username && x.IsDeleted == true);
        }
    }
}
