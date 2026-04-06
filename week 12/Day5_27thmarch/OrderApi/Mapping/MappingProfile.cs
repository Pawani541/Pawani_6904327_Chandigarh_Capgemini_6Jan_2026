using AutoMapper;
using OrderApi.Models;
using OrderApi.DTOs;

namespace OrderApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderDto, Order>();
    }
}