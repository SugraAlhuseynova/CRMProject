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
        private readonly AppDbContext _context;

        public UserService(UserManager<AppUser> userManager, IUserRepository userRepository, AppDbContext context)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _context = context;
        }
        public bool DeleteUser(string username)
        {
            var user = _userRepository.DeleteUser(username);
            if (user == null)
                return false;
            user.IsDeleted = true;
            if (user.Accounts != null)
            {
                foreach (var item in user.Accounts)
                {
                    item.IsDeleted = true;
                }
            }
            _context.SaveChanges();
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
                foreach (var item in user.Accounts)
                {
                    item.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return true;
        }
        public bool CheckUsernameExistence(string username)
        {
            return _userManager.Users.Any(x => x.UserName == username);
        }

        public async Task<AppUser> ReturnUserForLogin(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username && x.IsDeleted == false);
        }
    }
}
