using AutoMapper;
using Webapi.Entities;
using WebApi.Utilities.Enums;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBooks.GetBookCommand;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Utilities.Profiles;

class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, BookViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd.MM.yyyy")));
        CreateMap<Book, BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd.MM.yyyy")));
        CreateMap<UpdateBookModel, Book>();
    }
}