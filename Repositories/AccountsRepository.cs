﻿using CRM.DAL;
using CRM.DTO;
using CRM.Enums;
using CRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using CRM.Repositories.Interfaces;
using NuGet.Protocol.Core.Types;

namespace CRM.Repositories
{
    public class AccountsRepository : Repository<Account>, IAccountsRepository
    {
        public AccountsRepository(AppDbContext context) : base(context)
        {
        }

       
    }
}
