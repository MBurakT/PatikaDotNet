using System;
using System.Linq;
using AutoMapper;
using WebApi.Utilities.DBOperations;
using WebApi.Entities;

namespace WebApi.Utilities.Operations.GenreOperations.CreateGenres;

public class CreateGenreCommand
{
    readonly IBookStoreDbContext _context;
    readonly IMapper _mapper;

    public CreateGenreViewModel Genre { get; }

    public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper, CreateGenreViewModel genre)
    {
        _context = context;
        _mapper = mapper;
        Genre = genre;
    }

    public void Handle()
    {
        if (_context.Genres.Any(x => x.Name.Equals(Genre.Name))) throw new InvalidOperationException("Genre already exists!");

        _context.Genres.Add(_mapper.Map<CreateGenreViewModel, Genre>(Genre));
        _context.SaveChanges();
    }

    public class CreateGenreViewModel
    {
        public string? Name { get; set; }
    }
}