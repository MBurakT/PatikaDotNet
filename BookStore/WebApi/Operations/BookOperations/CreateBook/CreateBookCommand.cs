using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace WebApi.Operations.BookOperations.CreateBook;

public class CreateBookCommand
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;
    public CreateBookModel BookModel { get; }

    public CreateBookCommand(BookStoreDbContext context, IMapper mapper, CreateBookModel bookModel)
    {
        _context = context;
        _mapper = mapper;
        BookModel = bookModel;
    }

    public void Handle()
    {
        if (_context.Books.Any(x => x.Title == BookModel.Title)) throw new InvalidOperationException("Book already exist!");

        Book book = _mapper.Map<CreateBookModel, Book>(BookModel);

        if (!_context.Genres.Any(x => x.Id == book.GenreId)) throw new InvalidOperationException("Genre does not exist!");

        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public class CreateBookModel
    {
        public string? Title { get; set; }
        public int? PageCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? GenreId { get; set; }
    }
}