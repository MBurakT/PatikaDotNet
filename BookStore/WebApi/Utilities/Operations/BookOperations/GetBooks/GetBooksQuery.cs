using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Utilities.Operations.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        return _mapper.Map<List<Book>, List<BooksViewModel>>(
            _context.Books.Include(x => x.Author).Include(x => x.Genre).OrderBy(x => x.Title).ToList());
    }

    public class BooksViewModel()
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
}