using System;
using System.Linq;
using AutoMapper;
using Webapi.Entities;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks;

class GetBookCommand
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int Id { get; set; }

    public GetBookCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public BookViewModel Handle()
    {
        Book? book = _context.Books.SingleOrDefault(x => x.Id == Id);
        if (book == null) throw new InvalidOperationException("Book does not exist!");

        return _mapper.Map<Book, BookViewModel>(book);
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}