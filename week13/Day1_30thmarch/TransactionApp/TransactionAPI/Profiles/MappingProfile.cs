using AutoMapper;
using TransactionAPI.DTOs;
using TransactionAPI.Models;

namespace TransactionAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionInputDTO, Transaction>();
        }
    }
}
