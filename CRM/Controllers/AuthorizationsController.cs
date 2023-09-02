using AutoMapper;
using CRM.DTO;
using CRM.Models;
using CRM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthorizationsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AuthorizationsController(UserManager<AppUser> userManager,
                                        SignInManager<AppUser> signInManager,
                                        IConfiguration configuration,
                                        IMapper mapper,
                                        IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDto userDTO)
        {
            if ((_userService.CheckUsernameExistence(userDTO.UserName)))
                return BadRequest("username already taken");
            else
            {
                AppUser user = _mapper.Map<AppUser>(userDTO);
                user.Status = Enums.Status.Regular;
                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (!result.Succeeded)
                    return Ok(result.Errors);
                var data = result.Errors;
                return Ok(user);

            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto loginDTO)
        {
            AppUser user = await _userService.ReturnUserForLogin(loginDTO.Username);
            if (user is null)
                return NotFound("User not found");
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
                if (!result.Succeeded)
                    return BadRequest("Something wrong");
                else
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok("succesfully logged in");
                }
            }

        }
        [HttpPost("LoggedUser")]
        public async Task<IActionResult> FindCurrentUser()
        {
            AppUser user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Name)?.Value);
            return Ok(user);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            AppUser user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Email == forgotPasswordDto.Email && x.IsDeleted == false);
            if (user == null) { return NotFound("Email is incorrect"); }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            string url = $"{_configuration["reset"]}/ResetPassword?email={user.Email}&token={validToken}";

            //EmailService.SendEmail("alhuseynovasugra@gmail.com", "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
            //$"<p>To reset your password <a href='{url}'>Click here</a></p>");
            return Ok(validToken);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto passwordDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == passwordDto.Email && x.IsDeleted == false);
            if (user == null) return NotFound();
            var decodedToken = WebEncoders.Base64UrlDecode(passwordDto.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, passwordDto.NewPassword);
            if (!result.Succeeded)
                return BadRequest();
            return Ok("password successfully reset");
        }
    }
}
