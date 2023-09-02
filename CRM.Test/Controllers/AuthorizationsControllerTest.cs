using AutoMapper;
using CRM.Controllers;
using CRM.DTO;
using CRM.Models;
using CRM.Services.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CRM.Test.Controllers
{
    public class AuthorizationsControllerTest
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly AuthorizationsController _authorizationsController;
        public AuthorizationsControllerTest()
        {
            _userManager = A.Fake<UserManager<AppUser>>();
            _signInManager = A.Fake<SignInManager<AppUser>>();
            _configuration = A.Fake<IConfiguration>();
            _mapper = A.Fake<IMapper>();
            _userService = A.Fake<IUserService>();
            _authorizationsController = new(_userManager, _signInManager, _configuration, _mapper, _userService);
        }

        #region Tests of Register 
        [Fact]
        public async void Register_ReturnsUser_OkReceived()
        {
            //Arrange
            var userRegistrationDto = A.Fake<UserRegistrationDto>();
            A.CallTo(() => _userService.CheckUsernameExistence(userRegistrationDto.UserName)).Returns(false);
            AppUser user = A.Fake<AppUser>();
            A.CallTo(() => _mapper.Map<AppUser>(userRegistrationDto)).Returns(user);
            user.Status = Enums.Status.Regular;
            A.CallTo(() => _userManager.CreateAsync(user, userRegistrationDto.Password)).Returns(IdentityResult.Success);

            //act
            var actual = await _authorizationsController.Register(userRegistrationDto) as ObjectResult;
            var actualValue = actual.Value as AppUser;

            //Assert
            actual.StatusCode.Should().Be(200);
            actualValue.Should().Be(user);
        }
        [Fact]
        public async void Register_ReturnsIdentityResultErrors_OkReceived()
        {
            //Arrange
            var userRegistrationDto = A.Fake<UserRegistrationDto>();
            A.CallTo(() => _userService.CheckUsernameExistence(userRegistrationDto.UserName)).Returns(false);
            AppUser user = A.Fake<AppUser>();
            A.CallTo(() => _mapper.Map<AppUser>(userRegistrationDto)).Returns(user);
            user.Status = Enums.Status.Regular;
            var identityResult = A.Fake<IdentityResult>();
            A.CallTo(() => _userManager.CreateAsync(user, userRegistrationDto.Password)).Returns(IdentityResult.Failed());

            //Act
            var actual = await _authorizationsController.Register(userRegistrationDto) as ObjectResult;
            var actualValue = actual.Value as IEnumerable<IdentityError>;

            //Assert
            actual.StatusCode.Should().Be(200);
            actualValue.Should().BeEquivalentTo(identityResult.Errors);
        }

        [Fact]
        public async void Register_BadRequestReceived()
        {
            var expected = "username already taken";
            var userRegistrationDto = A.Fake<UserRegistrationDto>();
            A.CallTo(() => _userService.CheckUsernameExistence(userRegistrationDto.UserName)).Returns(true);

            var actual = await _authorizationsController.Register(userRegistrationDto) as ObjectResult;
            string actualValue = actual.Value as string;

            actual.StatusCode.Should().Be(400);
            actualValue.Should().BeEquivalentTo(expected);
        }
        #endregion

        #region Tests of Login
        [Fact]
        public async void Login_SingInSuccessfully_OkReceived()
        {
            //Arrange
            string expected = "succesfully logged in";
            UserLoginDto userLoginDto = A.Fake<UserLoginDto>();
            AppUser user = A.Fake<AppUser>();
            A.CallTo(() => _userService.ReturnUserForLogin(userLoginDto.Username)).Returns(user);
            A.CallTo(() => _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, false)).Returns(Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Act 
            var actual = await _authorizationsController.Login(userLoginDto) as ObjectResult;
            string actualValue = actual.Value as string;
            //Assert
            actual.StatusCode.Should().Be(200);
            actualValue.Should().Be(expected);
        }
        [Fact]
        public async void Login_SingInFailed_BadrequestReceived()
        {
            //Arrange
            string expected = "Something wrong";
            UserLoginDto userLoginDto = A.Fake<UserLoginDto>();
            AppUser user = A.Fake<AppUser>();
            A.CallTo(() => _userService.ReturnUserForLogin(userLoginDto.Username)).Returns(user);
            A.CallTo(() => _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, false)).Returns(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            //Act 
            var actual = await _authorizationsController.Login(userLoginDto) as ObjectResult;
            string actualValue = actual.Value as string;
            //Assert
            actual.StatusCode.Should().Be(400);
            actualValue.Should().Be(expected);
        }
        [Fact]
        public async void Login_UserNotFound_NotFoundReceived()
        {
            //Arrange
            string expected = "User not found";
            UserLoginDto userLoginDto = A.Fake<UserLoginDto>();
            AppUser user = null;
            A.CallTo(() => _userService.ReturnUserForLogin(userLoginDto.Username)).Returns(user);

            //Act 
            var actual = await _authorizationsController.Login(userLoginDto) as ObjectResult;
            string actualValue = actual.Value as string;
            //Assert
            actual.StatusCode.Should().Be(404);
            actualValue.Should().Be(expected);
        }
        #endregion



    }
}
