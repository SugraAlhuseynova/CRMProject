namespace CRM.Services.Interfaces
{
    public interface IUserService
    {
        bool DeleteUser(string username);
        bool RestoreUser(string username);

    }
}
