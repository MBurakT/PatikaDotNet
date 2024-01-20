using System;
using System.Linq;
using AutoMapper;
using Webapi.Entities;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int Id { get; set; }
    public UpdateBookModel BookModel { get; set; }

    public UpdateBookCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        if (!_context.Books.Any(x => x.Id == Id)) throw new Exception("Book does not exist!");

        Book book = _mapper.Map<UpdateBookModel, Book>(BookModel);
        book.Id = Id;

        _context.Books.Update(book);
        _context.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}