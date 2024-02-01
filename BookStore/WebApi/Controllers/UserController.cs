using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Operations.UserOperations.CreateTokens;
using WebApi.Operations.UserOperations.CreateUsers;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.UserOperations.CreateTokens;
using WebApi.Utilities.TokenOperations.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    readonly IBookStoreDbContext _context;
    readonly IMapper _mapper;
    readonly IConfiguration _configuration;

    public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserCommand.CreateUserModel userModel)
    {
        CreateUserCommand command = new(_context, _mapper, userModel);
        // CreateUserCommandValidator validator = new();

        // validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenCommand.CreateTokenModel tokenModel)
    {
        CreateTokenCommand command = new(_context, _mapper, _configuration, tokenModel);

        return command.Handle();
    }

    [HttpGet("refreshtoken")]
    public ActionResult<Token> RefreshToken([FromQuery] string refreshToken)
    {
        CreateRefreshTokenCommand command = new(_context, _configuration, new CreateRefreshTokenCommand.CreateRefreshTokenModel() { RefreshToken = refreshToken });

        return command.Handle();
    }
}