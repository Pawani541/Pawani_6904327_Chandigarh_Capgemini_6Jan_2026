using AutoMapper;
using UserAPI.Models;
using UserAPI.DTOs;

namespace UserAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<RegisterDTO, User>();
    }
}