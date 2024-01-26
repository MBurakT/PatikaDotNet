using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace WebApi.Utilities.Operations.AuthorOperations.CreateAuthors;

public class CreateAuthorCommand
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;

    public CreateAuthorViewModel Author { get; }

    public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper, CreateAuthorViewModel author)
    {
        _context = context;
        _mapper = mapper;
        Author = author;
    }

    public void Handle()
    {
        if (_context.Authors.Any(x => x.Name.Equals(Author.Name) && x.Surname.Equals(Author.Surname)))
            throw new InvalidOperationException("Author already exists!");

        _context.Authors.Add(_mapper.Map<CreateAuthorViewModel, Author>(Author));
        _context.SaveChanges();
    }

    public class CreateAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}