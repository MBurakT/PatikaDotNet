using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Webapi.Entities;
using WebApi.DBOperations;
using WebApi.Utilities.Enums;

namespace WebApi.BookOperations.GetBooks;

class GetBooksQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        return _mapper.Map<List<Book>, List<BooksViewModel>>(_context.Books.OrderBy(x => x.Title).ToList());
    }

    public class BooksViewModel()
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}