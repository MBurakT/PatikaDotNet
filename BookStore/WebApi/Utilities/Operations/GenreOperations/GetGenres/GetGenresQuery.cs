using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Utilities.DBOperations;
using WebApi.Entities;

namespace WebApi.Utilities.Operations.GenreOperations.GetGenres;

public class GetGenresQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<GenreQueryViewModel> Handle()
    {
        return _mapper.Map<List<Genre>, List<GenreQueryViewModel>>(_context.Genres.ToList());
    }

    public class GenreQueryViewModel
    {
        public string? Name { get; set; }
    }
}