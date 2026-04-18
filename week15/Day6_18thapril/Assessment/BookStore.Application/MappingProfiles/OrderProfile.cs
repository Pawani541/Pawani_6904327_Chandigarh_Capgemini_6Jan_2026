using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Order -> OrderResponseDto
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            // OrderItem -> OrderItemResponseDto
            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));

            // User -> UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
        }
    }
}
