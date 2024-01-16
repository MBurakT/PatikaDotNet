using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    // static List<Book> BookList = new List<Book>()
    // {
    //     new Book(1, "Lean Startup", 200, new DateTime(2001, 6, 12), 1),
    //     new Book(2, "Herland", 250, new DateTime(2010, 5, 23), 2),
    //     new Book(3, "Dune", 540, new DateTime(2002, 12, 21), 2)
    // };

    private readonly BookStoreDbContext _context;


    public BookController(BookStoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public List<Book> GetBooks()
    {
        return _context.Books.OrderBy(x => x.Title).ToList();
    }

    [HttpGet("{id:int}")]
    public Book? GetBookById(int id)
    {
        return _context.Books.SingleOrDefault(x => x.Id == id);
    }

    // [HttpGet]
    // public Book? GetBookByIdQuery([FromQuery] string id)
    // {
    //     return _context.Books.SingleOrDefault(x => x.Id == Convert.ToInt32(id));
    // }

    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        if (_context.Books.Any(x => x.Title == newBook.Title))
        {
            return BadRequest();
        }

        _context.Books.Add(newBook);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        Book? book = _context.Books.SingleOrDefault(x => x.Id == id);

        if (book == null)
        {
            return BadRequest();
        }

        if (updatedBook.Title != default) book.Title = updatedBook.Title;
        if (updatedBook.PageCount != default) book.PageCount = updatedBook.PageCount;
        if (updatedBook.PublishDate != default) book.PublishDate = updatedBook.PublishDate;
        if (updatedBook.GenreId != default) book.GenreId = updatedBook.GenreId;

        _context.Books.Update(book);
        _context.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        Book? book = _context.Books.SingleOrDefault(x => x.Id == id);

        if (book == null)
        {
            return BadRequest();
        }

        _context.Books.Remove(book);
        _context.SaveChanges();

        return Ok();
    }
}