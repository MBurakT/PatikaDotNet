using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace WebApi.Utilities.Operations.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public int Id { get; }
    public UpdateBookModel BookModel { get; }

    public UpdateBookCommand(IBookStoreDbContext context, IMapper mapper, int id, UpdateBookModel bookModel)
    {
        _context = context;
        _mapper = mapper;
        Id = id;
        BookModel = bookModel;

        Book? book = _context.Books.SingleOrDefault(x => x.Id == Id);

        if (book is null) throw new InvalidOperationException("Book does not exist!");

        _context.Books.Entry(book).State = EntityState.Detached;

        BookModel.Title = bookModel.Title ?? book.Title;
        BookModel.PageCount = bookModel.PageCount ?? book.PageCount;
        BookModel.PublishDate = bookModel.PublishDate ?? book.PublishDate;
        BookModel.AuthorId = bookModel.AuthorId ?? book.AuthorId;
        BookModel.GenreId = bookModel.GenreId ?? book.GenreId;
    }

    public void Handle()
    {
        if (!_context.Genres.Any(x => x.Id == BookModel.GenreId)) throw new InvalidOperationException("Genre does not exist!");

        if (!_context.Authors.Any(x => x.Id == BookModel.AuthorId)) throw new InvalidOperationException("Author does not exist!");

        Book book = _mapper.Map<UpdateBookModel, Book>(BookModel);
        book.Id = Id;

        _context.Books.Update(book);
        _context.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string? Title { get; set; }
        public int? PageCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? AuthorId { get; set; }
        public int? GenreId { get; set; }
    }
}