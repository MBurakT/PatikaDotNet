using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace WebApi.Utilities.Operations.AuthorOperations.GetAuthors;

public class GetAuthorsQuery
{
    readonly IBookStoreDbContext _context;
    readonly IMapper _mapper;

    public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<GetAuthorsViewModel> Handle()
    {
        return _mapper.Map<List<Author>, List<GetAuthorsViewModel>>(_context.Authors.OrderBy(x => x.Name).ToList());
    }

    public class GetAuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}