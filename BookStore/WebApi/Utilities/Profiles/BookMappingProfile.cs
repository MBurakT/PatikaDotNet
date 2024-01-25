using AutoMapper;
using WebApi.Entities;
using WebApi.Utilities.Enums;
using static WebApi.Operations.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.Operations.BookOperations.GetBooks.GetBookCommand;
using static WebApi.Operations.BookOperations.GetBooks.GetBooksQuery;
using static WebApi.Operations.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Utilities.Profiles;

class BookMappingProfile : Profile
{
    public BookMappingProfile()
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