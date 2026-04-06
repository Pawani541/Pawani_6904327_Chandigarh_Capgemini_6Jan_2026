using AutoMapper;
using BankingAPI.Models;
using BankingAPI.DTOs;

namespace BankingAPI.Mapping   
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, UserAccountDTO>()
                .ForMember(dest => dest.MaskedAccountNumber,
                    opt => opt.MapFrom(src =>
                        src.AccountNumber.Length > 4
                        ? new string('X', src.AccountNumber.Length - 4) +
                          src.AccountNumber.Substring(src.AccountNumber.Length - 4)
                        : src.AccountNumber));

            CreateMap<Account, AdminAccountDTO>();
        }
    }
}