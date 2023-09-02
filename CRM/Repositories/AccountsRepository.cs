using CRM.DAL;
using CRM.DTO;
using CRM.Enums;
using CRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using CRM.Repositories.Interfaces;

namespace CRM.Repositories
{
    public class AccountsRepository : Repository<Account>, IAccountsRepository
    {
        public AccountsRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Account> GetAccountIsdeletedFalseAsync(int id)
        {
            return await GetAsync(x => x.Id == id && !x.IsDeleted && !x.IsBlocked);
        }
    }
}
