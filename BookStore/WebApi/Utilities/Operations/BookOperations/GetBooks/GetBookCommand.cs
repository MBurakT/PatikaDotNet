using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Utilities.Operations.BookOperations.GetBooks;

public class GetBookCommand
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int Id { get; set; }

    public GetBookCommand(IBookStoreDbContext context, IMapper mapper, int id)
    {
        _context = context;
        _mapper = mapper;
        Id = id;
    }

    public BookViewModel Handle()
    {
        Book? book = _context.Books.Where(x => x.Id == Id).Include(x => x.Author).Include(x => x.Genre).SingleOrDefault();
        if (book == null) throw new InvalidOperationException("Book does not exist!");

        return _mapper.Map<Book, BookViewModel>(book);
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
}