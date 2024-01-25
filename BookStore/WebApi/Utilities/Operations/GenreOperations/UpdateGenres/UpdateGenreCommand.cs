using System;
using System.Linq;
using AutoMapper;
using WebApi.Utilities.DBOperations;
using WebApi.Entities;

namespace WebApi.Utilities.Operations.GenreOperations.UpdateGenres;

public class UpdateGenreCommand
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;
    public int Id { get; }
    public UpdateGenreViewModel Genre { get; }

    public UpdateGenreCommand(BookStoreDbContext context, IMapper mapper, int id, UpdateGenreViewModel genre)
    {
        _context = context;
        _mapper = mapper;
        Id = id;
        Genre = genre;
    }

    public void Handle()
    {
        if (!_context.Genres.Any(x => x.Id == Id)) throw new InvalidOperationException("Genre does not exist!");

        Genre genre = _mapper.Map<UpdateGenreViewModel, Genre>(Genre);
        genre.Id = Id;

        _context.Genres.Update(genre);
        _context.SaveChanges();
    }

    public class UpdateGenreViewModel
    {
        public string? Name { get; set; }
    }
}