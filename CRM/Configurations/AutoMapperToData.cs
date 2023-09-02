using AutoMapper;
using AutoMapper.Features;
using CRM.DTO;
using CRM.Models;
using CRM.TransactionModels;

namespace CRM.Configurations
{
    public class AutoMapperToData:Profile
    {
        public AutoMapperToData()
        {
            CreateMap<Account, GetAccountDto>()
                 .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.User.Name+" "+src.User.LastName));
            CreateMap<AccountDto, Account>();
            CreateMap<UserRegistrationDto, AppUser>();
            CreateMap<TransferTransactionModel, TransactionModel>().ReverseMap();
        }
    }
}
