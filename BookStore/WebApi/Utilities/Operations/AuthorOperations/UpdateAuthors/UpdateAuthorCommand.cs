using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace WebApi.Utilities.Operations.AuthorOperations.UpdateAuthors;

public class UpdateAuthorCommand
{
    readonly IBookStoreDbContext _context;
    readonly IMapper _mapper;

    public int Id { get; }
    public UpdateAuthorViewModel Author { get; }

    public UpdateAuthorCommand(IBookStoreDbContext context, IMapper mapper, UpdateAuthorViewModel author, int id)
    {
        _context = context;
        _mapper = mapper;
        Author = author;
        Id = id;
    }

    public void Handle()
    {
        if (!_context.Authors.Any(x => x.Id == Id)) throw new InvalidOperationException("Author does not exist!");

        Author author = _mapper.Map<UpdateAuthorViewModel, Author>(Author);
        author.Id = Id;

        _context.Authors.Update(author);
        _context.SaveChanges();
    }

    public class UpdateAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}