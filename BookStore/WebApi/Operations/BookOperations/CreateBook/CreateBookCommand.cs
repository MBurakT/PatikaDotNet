using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.CreateBook;

public class CreateBookCommand
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateBookModel BookModel { get; set; }

    public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        if (_context.Books.Any(x => x.Title == BookModel.Title))
        {
            throw new InvalidOperationException("Book already exist!");
        }

        Book book = _mapper.Map<CreateBookModel, Book>(BookModel);

        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}