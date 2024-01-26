using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace WebApi.Utilities.Operations.AuthorOperations.DeleteAuthors;

public class DeleteAuthorCommand
{
    readonly BookStoreDbContext _context;

    public int Id { get; }

    public DeleteAuthorCommand(BookStoreDbContext context, int id)
    {
        _context = context;
        Id = id;
    }

    public void Handle()
    {
        Author? author = _context.Authors.Where(x => x.Id == Id).Include(x => x.Books).SingleOrDefault();

        if (author is null) throw new InvalidOperationException("Author does not exist!");

        _context.Authors.Remove(author);
        _context.SaveChanges();
    }
}