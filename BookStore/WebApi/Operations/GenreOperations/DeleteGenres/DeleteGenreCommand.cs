using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.GenreOperations.DeleteGenres;

public class DeleteGenreCommand
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;
    public int Id { get; }

    public DeleteGenreCommand(BookStoreDbContext context, IMapper mapper, int id)
    {
        _context = context;
        _mapper = mapper;
        Id = id;
    }

    public void Handle()
    {
        Genre? genre = _context.Genres.SingleOrDefault(x => x.Id == Id);

        if (genre is null) throw new InvalidOperationException("Genre does not exist!");

        _context.Genres.Remove(genre);
        _context.SaveChanges();
    }
}