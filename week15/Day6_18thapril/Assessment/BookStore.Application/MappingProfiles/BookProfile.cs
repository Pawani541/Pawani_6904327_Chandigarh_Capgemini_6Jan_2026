using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            // Book -> BookDto
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name));

            // BookCreateDto -> Book
            CreateMap<BookCreateDto, Book>();

            // BookUpdateDto -> Book
            CreateMap<BookUpdateDto, Book>();

            // Category -> CategoryDto
            CreateMap<Category, CategoryDto>().ReverseMap();

            // Author -> AuthorDto
            CreateMap<Author, AuthorDto>().ReverseMap();

            // Review -> ReviewDto
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName));

            // Wishlist -> WishlistDto
            CreateMap<Wishlist, WishlistDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Book.Price))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Book.ImageUrl));
        }
    }
}
