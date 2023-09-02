using Microsoft.AspNetCore.Mvc;
using CRM.DTO;
using CRM.Services.Interface;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        // GET: api/Accounts
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(_accountsService.GetAll());
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            return Ok(await _accountsService.GetAsync(id));
        }

        [HttpPost]
        public async Task CreateAccount(AccountDto accountDto)
        {
            await _accountsService.CreateAsync(accountDto);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task DeleteAccount(int id)
        {
            await _accountsService.Delete(id);
        }

        [HttpPatch("changeCurrency")]
        public async Task ChangeCurrency(int id, byte currency)
        {
           await _accountsService.ChangeCurrency(id, currency);
        }
        [HttpPut("block")]
        public async Task BlockAccount(int id)
        {
            await _accountsService.BlockAccount(id);
        }
        [HttpPut("unBlock")]
        public async Task UnBlockAccount(int id)
        {
            await _accountsService.UnBlockAccount(id);
        }
    }
}