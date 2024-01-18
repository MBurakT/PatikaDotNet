using System;
using System.Linq;
using Webapi.Entities;
using WebApi.DBOperations;
using WebApi.Enums;

namespace WebApi.BookOperations.GetBooks;

class GetBookCommand
{
    private readonly BookStoreDbContext _context;
    public int Id { get; set; }

    public GetBookCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public BooksViewModel Handle()
    {
        Book? book = _context.Books.SingleOrDefault(x => x.Id == Id);
        if (book == null) throw new InvalidOperationException("Book does not exist!");

        return new BooksViewModel
        {
            Title = book.Title,
            PageCount = book.PageCount,
            PublishDate = book.PublishDate.Date.ToString("dd.MM.yyy"),
            Genre = ((GenreEnum)book.GenreId).ToString()
        };
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}