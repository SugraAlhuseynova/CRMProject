using CRM.DAL;
using CRM.Models;
using CRM.Repositories.Interfaces;
using CRM.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Test.Services
{
    public class UserServiceTest
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly UserService _sut;

        public UserServiceTest()
        {
            _userManager = A.Fake<UserManager<AppUser>>();
            _userRepository = A.Fake<IUserRepository>();
            _sut = new UserService(_userManager, _userRepository); 

        }

        [Theory]
        [InlineData("name",false)]
        public void DeleteUser_ReturnFalse_InvalidUsername(string username, bool expected)
        {
            //AppUser user = null;
            A.CallTo(() => _userRepository.DeleteUser(username)).Returns(null);
            bool actual = _sut.DeleteUser(username);
            
            actual.Should().Be(expected);
        }
        [Theory]
        [InlineData("name", true)]
        public void DeleteUser_ReturnTrue_ValidUsername(string username, bool expected)
        {
            AppUser user = A.Fake<AppUser>();
            A.CallTo(() => _userRepository.DeleteUser(username)).Returns(user);
            user.IsDeleted = true;

            bool actual = _sut.DeleteUser(username);

            actual.Should().Be(expected);
        }
        [Theory]
        [InlineData("name", true)]
        public void RestoreUser_ReturnTrue_ValidUsername(string username, bool expected)
        {
            AppUser user = A.Fake<AppUser>();
            A.CallTo(() => _userRepository.RestoreUser(username)).Returns(user);
            user.IsDeleted = false;

            bool actual = _sut.RestoreUser(username);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("name", false)]
        public void RestoreUser_ReturnFalse_InvalidUsername(string username, bool expected)
        {
            //AppUser user = null;
            A.CallTo(() => _userRepository.RestoreUser(username)).Returns(null);
            bool actual = _sut.RestoreUser(username);

            actual.Should().Be(expected);
        }
    }
}
