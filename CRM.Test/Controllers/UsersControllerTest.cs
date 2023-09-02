using CRM.Controllers;
using CRM.Services.Interfaces;
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
    public class UsersControllerTest
    {
        private readonly IUserService _userService;
        private readonly UsersController _usersController;

        public UsersControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _usersController= new(_userService);
        }

        [Theory]
        [InlineData("Sugra")]
        public void RestoreUser_RecievedOk(string username)
        {
            string expected = $"User {username} is successfully restored";
            A.CallTo(() => _userService.RestoreUser(username)).Returns(true);
            var actual = _usersController.RestoreUser(username) as ObjectResult;
            string actualValue = actual.Value as string;
            actual.StatusCode.Should().Be(200);
            actualValue.Should().Be(expected);
        }
        [Theory]
        [InlineData("")]
        public void RestoreUser_RecievedNotFound(string username)
        {
            string expected = $"{username} is not found";
            A.CallTo(() => _userService.RestoreUser(username)).Returns(false);
            var actual = _usersController.RestoreUser(username) as ObjectResult;
            string actualValue = actual.Value as string;
            actual.StatusCode.Should().Be(404);
            actualValue.Should().Be(expected);
        }
        [Theory]
        [InlineData("Sugra")]
        public void DeleteUser_RecievedOk(string username)
        {
            string expected = $"User {username} is successfully deleted";
            A.CallTo(() => _userService.DeleteUser(username)).Returns(true);
            var actual = _usersController.DeleteUser(username) as ObjectResult;
            string actualValue = actual.Value as string;
            actual.StatusCode.Should().Be(200);
            actualValue.Should().Be(expected);
        }
        [Theory]
        [InlineData("")]
        public void DeleteUser_RecievedNotFound(string username)
        {
            string expected = $"{username} is not found";
            A.CallTo(() => _userService.DeleteUser(username)).Returns(false);
            var actual = _usersController.DeleteUser(username) as ObjectResult;
            string actualValue = actual.Value as string;
            actual.StatusCode.Should().Be(404);
            actualValue.Should().Be(expected);
        }
    }
}
