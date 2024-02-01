using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.GenreOperations.GetGenres;
using WebApi.Utilities.Operations.GenreOperations.CreateGenres;
using WebApi.Utilities.Operations.GenreOperations.UpdateGenres;
using WebApi.Utilities.Operations.GenreOperations.DeleteGenres;
using WebApi.Utilities.Validators.GenreValidators;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]s")]
public class GenreController : ControllerBase
{
    readonly IBookStoreDbContext _context;
    readonly IMapper _mapper;

    public GenreController(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new(_context, _mapper);

        return Ok(query.Handle());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetGenreById(int id)
    {
        GetGenreCommand command = new(_context, _mapper, id);
        GetGenreCommandValidator validator = new();

        validator.ValidateAndThrow(command);

        return Ok(command.Handle());
    }

    [HttpPost]
    public IActionResult CreateGenre([FromBody] CreateGenreCommand.CreateGenreViewModel genreModel)
    {
        CreateGenreCommand command = new(_context, _mapper, genreModel);
        CreateGenreCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreCommand.UpdateGenreViewModel genre)
    {
        UpdateGenreCommand command = new(_context, _mapper, id, genre);
        UpdateGenreCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteGenre(int id)
    {
        DeleteGenreCommand command = new(_context, id);
        DeleteGenreCommandValidator validator = new();

        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }
}