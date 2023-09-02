using CRM.Controllers;
using CRM.DTO;
using CRM.Services;
using CRM.Services.Interface;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Test.Controllers
{
    public class AccountsControllerTest
    {
        private readonly IAccountsService _accountsService;
        private readonly AccountsController _accountsController;

        public AccountsControllerTest()
        {
            _accountsService = A.Fake<IAccountsService>();
            _accountsController = new(_accountsService);
        }


        [Fact]
        public void GetAccounts_ReturnsAccountGetDtoList_OkRecieved()
        {
            //Arrange
            var expected = A.Fake<List<GetAccountDto>>();
            A.CallTo(() => _accountsService.GetAll()).Returns(expected);

            //Act
            var actual = _accountsController.GetAccounts() as ObjectResult;
            var actualValue = actual.Value as List<GetAccountDto>;
            //Assert
            actual.Should().NotBeNull();
            actual.StatusCode.Should().Be(200);
            actualValue.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async void GetAccountById_ReturnsAccount_OkRecieved()
        {
            int id = 1;
            var expected = A.Fake<GetAccountDto>();
            A.CallTo(() => _accountsService.GetAsync(id)).Returns(expected);

            var actual = await _accountsController.GetAccount(id) as ObjectResult;
            var actualValue = actual.Value as GetAccountDto;

            actual.Should().NotBeNull();
            actual.StatusCode.Should().Be(200);
            actualValue.Should().BeEquivalentTo(expected);
        }

    }
}
