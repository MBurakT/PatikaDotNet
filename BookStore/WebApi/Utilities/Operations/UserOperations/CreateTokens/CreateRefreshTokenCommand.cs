using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.TokenOperations;
using WebApi.Utilities.TokenOperations.Models;

namespace WebApi.Utilities.Operations.UserOperations.CreateTokens;

public class CreateRefreshTokenCommand
{
    readonly IBookStoreDbContext _context;
    readonly IConfiguration _configuration;

    public CreateRefreshTokenModel RefreshTokenModel { get; }

    public CreateRefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration, CreateRefreshTokenModel refreshTokenModel)
    {
        _context = context;
        _configuration = configuration;
        RefreshTokenModel = refreshTokenModel;
    }

    public Token Handle()
    {
        User? user = _context.Users.SingleOrDefault(x => x.RefreshToken.Equals(RefreshTokenModel.RefreshToken) && x.RefreshTokenExpireDate > DateTime.Now);

        if (user is null) throw new InvalidOperationException("Refresh token is invalid or expired!");

        TokenHandler tokenHandler = new(_configuration);

        Token token = tokenHandler.CreateAccessToken();

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        _context.SaveChanges();

        return token;
    }

    public class CreateRefreshTokenModel
    {
        public string? RefreshToken { get; set; }
    }
}