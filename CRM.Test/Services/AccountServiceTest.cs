using AutoMapper;
using CRM.DTO;
using CRM.Enums;
using CRM.Exceptions;
using CRM.Models;
using CRM.Repositories.Interfaces;
using CRM.Services;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Test.Services
{
    public class AccountServiceTest
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IMapper _mapper;
        private readonly AccountsService _sut;
        public AccountServiceTest()
        {
            _accountsRepository = A.Fake<IAccountsRepository>();
            _mapper = A.Fake<IMapper>(); 
            _sut = new AccountsService(_accountsRepository, _mapper);
        }

        [Fact]
        public async Task BlockAccount_ShouldFailAsync()
        {
            int id = 1;
            Account account = null;
            string expected = "item not found";
            A.CallTo(() => _accountsRepository.GetAccountIsdeletedFalseAsync(id)).Returns(account);
            Func<Task> action = async () => await _sut.BlockAccount(id);
            await action.Should().ThrowAsync<ItemNotFoundException>().WithMessage(expected);
        }






    }
}
