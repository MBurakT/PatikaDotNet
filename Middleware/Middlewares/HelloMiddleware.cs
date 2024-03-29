using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Middleware.Middlewares;

public class HelloMiddleware
{
    private readonly RequestDelegate _next;

    public HelloMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine("Hello, World!");
        await _next.Invoke(context);
        Console.WriteLine("Hello, Middleware!");
    }
}

public static class HelloMiddlewareExtensions
{
    public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HelloMiddleware>();
    }
}