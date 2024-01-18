using System;
using System.Linq;
using Webapi.Entities;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    private readonly BookStoreDbContext _context;
    public CreateBookModel BookModel { get; set; }

    public CreateBookCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        if (_context.Books.Any(x => x.Title == BookModel.Title))
        {
            throw new InvalidOperationException("Book already exist!");
        }

        Book book = new Book(BookModel.Title, BookModel.PageCount, BookModel.PublishDate, BookModel.GenreId);

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