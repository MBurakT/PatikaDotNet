using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.AuthorOperations.CreateAuthors;
using WebApi.Utilities.Operations.AuthorOperations.DeleteAuthors;
using WebApi.Utilities.Operations.AuthorOperations.GetAuthors;
using WebApi.Utilities.Operations.AuthorOperations.UpdateAuthors;
using WebApi.Utilities.Validators.AuthorValidators;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    readonly IBookStoreDbContext _context;
    readonly IMapper _mapper;

    public AuthorController(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorsQuery query = new(_context, _mapper);

        return Ok(query.Handle());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAuthorById(int id)
    {
        GetAuthorCommand command = new(_context, _mapper, id);
        GetAuthorCommandValidator validator = new();

        validator.ValidateAndThrow(command);

        return Ok(command.Handle());
    }

    [HttpPost]
    public IActionResult AddAuthor([FromBody] CreateAuthorCommand.CreateAuthorViewModel author)
    {
        CreateAuthorCommand command = new(_context, _mapper, author);
        CreateAuthorCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorCommand.UpdateAuthorViewModel author)
    {
        UpdateAuthorCommand command = new(_context, _mapper, author, id);
        UpdateAuthorCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAuthor(int id)
    {
        DeleteAuthorCommand command = new(_context, id);
        DeleteAuthorCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }
}