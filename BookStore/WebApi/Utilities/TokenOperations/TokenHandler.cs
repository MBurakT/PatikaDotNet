using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Utilities.TokenOperations.Models;

namespace WebApi.Utilities.TokenOperations;

public class TokenHandler
{
    readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateAccessToken()
    {
        Token token = new();
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.Now.AddMinutes(15);

        JwtSecurityToken securityToken = new
        (
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            expires: token.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: credentials
        );

        JwtSecurityTokenHandler securityTokenHandler = new();

        token.AccessToken = securityTokenHandler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshToken();

        return token;
    }

    string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}