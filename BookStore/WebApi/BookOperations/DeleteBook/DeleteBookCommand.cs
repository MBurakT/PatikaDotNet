using System;
using System.Linq;
using Webapi.Entities;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook;

class DeleteBookCommand
{
    private readonly BookStoreDbContext _context;

    public DeleteBookCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle(int id)
    {
        Book? book = _context.Books.SingleOrDefault(x => x.Id == id);

        if (book == null) throw new Exception("Book does not exist!");

        _context.Books.Remove(book);
        _context.SaveChanges();
    }
}