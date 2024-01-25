using System;
using System.Linq;
using WebApi.Entities;
using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.DeleteBook;

class DeleteBookCommand
{
    private readonly BookStoreDbContext _context;
    public int Id { get; }

    public DeleteBookCommand(BookStoreDbContext context, int id)
    {
        _context = context;
        Id = id;
    }

    public void Handle()
    {
        Book? book = _context.Books.SingleOrDefault(x => x.Id == Id);

        if (book == null) throw new Exception("Book does not exist!");

        _context.Books.Remove(book);
        _context.SaveChanges();
    }
}