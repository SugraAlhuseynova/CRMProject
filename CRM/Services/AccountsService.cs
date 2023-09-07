using CRM.DTO;
using CRM.Enums;
using CRM.Models;
using CRM.Repositories.Interfaces;
using CRM.Services.Interface;
using AutoMapper;
using CRM.Exceptions;

namespace CRM.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IMapper _mapper;

        public AccountsService(IAccountsRepository accountsRepository, 
            IMapper mapper) 
        {
            _accountsRepository = accountsRepository;
            _mapper = mapper;
        }

        public async Task BlockAccount(int id)
        {
            var entity = await _accountsRepository.GetAccountIsdeletedFalseAsync(id);
            if (entity == null)
                throw new ItemNotFoundException("item not found");
            entity.IsBlocked = true;
            entity.LockDate = DateTime.UtcNow.AddHours(4);
            await _accountsRepository.CommitAsync();
        }

        public async Task ChangeCurrency(int id, byte currency)
        {
            var entity = await _accountsRepository.GetAccountIsdeletedFalseAsync(id);
            if (entity == null)
                throw new ItemNotFoundException("item not found");
            if (Enum.IsDefined(typeof(Currency), currency))
            {
                entity.Currency = (Currency)currency;
                await _accountsRepository.CommitAsync();
            }
            else
                throw new ItemNotFoundException("Currency not found");
        }

        public async Task CreateAsync(AccountDto postDto)
        {
            Account entity = await _accountsRepository.GetAsync(x=>x.Name == postDto.Name && x.Currency == postDto.Currency && !x.IsDeleted);
            if (entity != null)
                throw new RecordDuplicateException("Item already exist");
            entity = _mapper.Map<Account>(postDto);
            await _accountsRepository.CreateAsync(entity);
            await _accountsRepository.CommitAsync();    
        }

        public async Task Delete(int id)
        {
            var entity = await _accountsRepository.GetAccountIsdeletedFalseAsync(id);
            if (entity == null)
                throw new ItemNotFoundException("item not found");
            entity.IsDeleted = true;
            await _accountsRepository.CommitAsync();
        }

        public List<GetAccountDto> GetAll()
        {
            var entity = _accountsRepository.GetAll(x => !x.IsDeleted);
            List<GetAccountDto> accountsDto = _mapper.Map<List<GetAccountDto>>(entity);
            return accountsDto;
        }

        public async Task<GetAccountDto> GetAsync(int id)
        {
            var entity = await _accountsRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "User");
            if (entity == null)
                throw new ItemNotFoundException("item not found");
            GetAccountDto accountDto = _mapper.Map<GetAccountDto>(entity);
            return accountDto;
        }

        public async Task UnBlockAccount(int id)
        {
            var entity = await _accountsRepository.GetAsync(x => x.Id == id && !x.IsDeleted && x.IsBlocked);
            if (entity == null)
                throw new ItemNotFoundException("item not found");
            entity.IsBlocked = false;
            entity.LockDate = DateTime.UtcNow.AddHours(4);
            await _accountsRepository.CommitAsync();
        }

        public async Task UpdateAsync(int id, AccountDto postDto)
        {
            var entity = await _accountsRepository.GetAccountIsdeletedFalseAsync(id);
            if (entity == null)
                throw new ItemNotFoundException("item not found");
            if (await _accountsRepository.IsExistAsync(x => x.Name == postDto.Name && x.Currency == postDto.Currency && !x.IsDeleted))
                throw new RecordDuplicateException("Item already exist");
            
            if (Enum.IsDefined(typeof(Currency), postDto.Currency))
            {
                entity.Currency = (Currency)postDto.Currency;
                entity.Name = postDto.Name;
                await _accountsRepository.CommitAsync();
            }

        }

      

        //public GetAccountDto GetAccount(int id)
        //{
        //    var result = _accountsRepository.GetAccount(id);
        //    if (result != null)
        //        //_mapper.Map<Account, GetAccountDto>(result);
        //        return _mapper.Map<Account, GetAccountDto>(result);
        //    return null;
        //}

        //public List<GetAccountDto> GetAccounts()
        //{
        //    var result = _accountsRepository.GetAccounts();
        //    if (result != null)
        //        return _mapper.Map<List<GetAccountDto>>(result);
        //    else return null;
        //}

        //public bool ChangeCurrency(int id, byte currency)
        //{
        //    var result = _accountsRepository.ChangeCurrency(id);
        //    if (result == null) { return false; }
        //    if (Enum.IsDefined(typeof(Currency), currency))
        //    {
        //        result.Currency = (Currency)currency;
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    else return false;
        //}

    }
}
