using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int Id { get; }
    public UpdateBookModel BookModel { get; }

    public UpdateBookCommand(BookStoreDbContext context, IMapper mapper, int id, UpdateBookModel bookModel)
    {
        _context = context;
        _mapper = mapper;
        Id = id;
        BookModel = bookModel;
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