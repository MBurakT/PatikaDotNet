using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Utilities.DBOperations;
using WebApi.Entities;
using static WebApi.Utilities.Operations.BookOperations.GetBooks.GetBookCommand;

namespace WebApi.Utilities.Operations.GenreOperations.GetGenres;

public class GetGenreCommand
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;
    public int Id { get; }

    public GetGenreCommand(BookStoreDbContext context, IMapper mapper, int id)
    {
        _context = context;
        _mapper = mapper;
        Id = id;
    }

    public GenreCommandViewModel Handle()
    {
        Genre? genre = _context.Genres.Where(x => x.Id == Id).Include(x => x.Books).SingleOrDefault();

        if (genre is null) throw new InvalidOperationException("Genre does not exist!");

        return _mapper.Map<Genre, GenreCommandViewModel>(genre);
    }

    public class GenreCommandViewModel
    {
        public string? Name { get; set; }
        public ICollection<BookViewModel>? Books { get; set; }
    }
}