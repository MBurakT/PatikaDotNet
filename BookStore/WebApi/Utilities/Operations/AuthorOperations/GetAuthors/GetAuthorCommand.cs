using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;
using static WebApi.Utilities.Operations.BookOperations.GetBooks.GetBooksQuery;

namespace WebApi.Utilities.Operations.AuthorOperations.GetAuthors;

public class GetAuthorCommand
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;
    public int Id { get; }

    public GetAuthorCommand(BookStoreDbContext context, IMapper mapper, int id)
    {
        _context = context;
        _mapper = mapper;
        Id = id;
    }

    public GetAuthorViewModel Handle()
    {
        Author? author = _context.Authors.Where(x => x.Id == Id).Include(x => x.Books).SingleOrDefault();

        if (author is null) throw new InvalidOperationException("Author does not exist!");

        return _mapper.Map<Author, GetAuthorViewModel>(author);
    }

    public class GetAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<BooksViewModel> Books { get; set; }
    }
}