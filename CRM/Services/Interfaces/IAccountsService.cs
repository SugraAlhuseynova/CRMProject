using CRM.DTO;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface IAccountsService
    {
        Task ChangeCurrency(int id, byte currency);
        Task BlockAccount(int id);
        Task UnBlockAccount(int id);
        Task CreateAsync(AccountDto postDto);
        Task UpdateAsync(int id, AccountDto postDto);
        Task<GetAccountDto> GetAsync(int id);
        Task Delete(int id);
        List<GetAccountDto> GetAll();

    }
}
