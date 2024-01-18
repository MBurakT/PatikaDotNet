using System;
using System.Linq;
using Webapi.Entities;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext _context;
    public int Id { get; set; }
    public UpdateBookModel BookModel { get; set; }

    public UpdateBookCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        Book? book = _context.Books.SingleOrDefault(x => x.Id == Id);

        if (book == null) throw new Exception("Book does not exist!");

        if (BookModel.Title != default) book.Title = BookModel.Title;
        if (BookModel.PageCount != default) book.PageCount = BookModel.PageCount;
        if (BookModel.PublishDate != default) book.PublishDate = BookModel.PublishDate;
        if (BookModel.GenreId != default) book.GenreId = BookModel.GenreId;

        _context.Books.Update(book);
        _context.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}