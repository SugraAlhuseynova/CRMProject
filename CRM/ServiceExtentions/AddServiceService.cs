using CRM.Configurations;
using CRM.Repositories.Interfaces;
using CRM.Repositories;
using CRM.Services.Interface;
using CRM.Services.Interfaces;
using CRM.Services;

namespace CRM.ServiceExtentions
{
    public static class AddServiceService
    {
        public static void AddService(this WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<IAccountsService, AccountsService>();
            builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddAutoMapper(typeof(AutoMapperToData));

        }
    }
}
