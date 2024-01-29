using System;
using System.Linq;
using WebApi.Utilities.DBOperations;
using WebApi.Entities;

namespace WebApi.Utilities.Operations.GenreOperations.DeleteGenres;

public class DeleteGenreCommand
{
    readonly IBookStoreDbContext _context;

    public int Id { get; }

    public DeleteGenreCommand(IBookStoreDbContext context, int id)
    {
        _context = context;
        Id = id;
    }

    public void Handle()
    {
        Genre? genre = _context.Genres.SingleOrDefault(x => x.Id == Id);

        if (genre is null) throw new InvalidOperationException("Genre does not exist!");

        _context.Books.Where(x => x.GenreId == Id).ToList().ForEach(x => x.GenreId = 1);
        _context.Genres.Remove(genre);
        _context.SaveChanges();
    }
}