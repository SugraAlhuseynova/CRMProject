using CRM.DAL;
using CRM.Models;
using CRM.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // Restore: api/Users/name
        [HttpPut("RestoreUser")]
        public IActionResult RestoreUser(string username)
        {
            var result = _userService.RestoreUser(username);
            if (!result) return NotFound($"{username} is not found");
            else return Ok($"User {username} is successfully restored");
        }

        // DELETE: api/Users/name
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(string username)
        {
            var result = _userService.DeleteUser(username);
            if (!result) return NotFound($"{username} is not found");
            else return Ok($"User {username} is successfully deleted");
        }
    }
}