using CRM.Models;

namespace CRM.Repositories.Interfaces
{
    public interface IUserRepository
    {
        AppUser DeleteUser(string username);
        AppUser RestoreUser(string username);
    }
}
