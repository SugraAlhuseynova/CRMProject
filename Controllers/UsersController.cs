using CRM.DAL;
using CRM.DTO;
using CRM.Email;
using CRM.Enums;
using CRM.Models;
using CRM.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration,
            AppDbContext context, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _userService = userService;
        }
        // Restore: api/Users/name
        [HttpPut("RestoreUser")]
        public async Task<IActionResult> RestoreUser(string username)
        {
            var result = _userService.RestoreUser(username);
            if (!result) return NotFound($"{username} is not found");
            else return Ok($"User {username} is successfully restored");
        }

        // DELETE: api/Users/name
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var result = _userService.DeleteUser(username);
            if (!result) return NotFound($"{username} is not found");
            else return Ok($"User {username} is successfully deleted");
        }


    }
}