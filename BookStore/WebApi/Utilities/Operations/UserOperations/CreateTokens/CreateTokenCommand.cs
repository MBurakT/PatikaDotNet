using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.TokenOperations;
using WebApi.Utilities.TokenOperations.Models;

namespace WebApi.Operations.UserOperations.CreateTokens;

public class CreateTokenCommand
{
    readonly IBookStoreDbContext _context;
    readonly IMapper _mapper;
    readonly IConfiguration _configuration;

    public CreateTokenModel TokenModel { get; }

    public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration, CreateTokenModel tokenModel)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
        TokenModel = tokenModel;
    }

    public Token Handle()
    {
        User? user = _context.Users.SingleOrDefault(x => x.Email.Equals(TokenModel.Email) && x.Password.Equals(TokenModel.Password));

        if (user is null) throw new InvalidOperationException("Invalid credentials!");

        TokenHandler tokenHandler = new(_configuration);

        Token token = tokenHandler.CreateAccessToken();

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        _context.SaveChanges();

        return token;
    }

    public class CreateTokenModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}