using CRM.DAL;
using CRM.Models;
using CRM.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public UserRepository(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
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
