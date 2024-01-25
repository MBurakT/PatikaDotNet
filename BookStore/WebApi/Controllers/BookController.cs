using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Operations.BookOperations.CreateBook;
using WebApi.Operations.BookOperations.DeleteBook;
using WebApi.Operations.BookOperations.GetBooks;
using WebApi.Operations.BookOperations.UpdateBook;
using WebApi.BookOpertions.CreateBook;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new(_context, _mapper);

        return Ok(query.Handle());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBookById(int id)
    {
        GetBookCommand command = new(_context, _mapper, id);
        GetBookCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        return Ok(command.Handle());
    }

    // [HttpGet]
    // public Book? GetBookByIdQuery([FromQuery] string id)
    // {
    //     return _context.Books.SingleOrDefault(x => x.Id == Convert.ToInt32(id));
    // }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
    {
        CreateBookCommand command = new(_context, _mapper, newBook);
        CreateBookCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookCommand.UpdateBookModel updatedBook)
    {
        UpdateBookCommand command = new(_context, _mapper, id, updatedBook);
        UpdateBookCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command = new(_context, id);
        DeleteBookCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }
}