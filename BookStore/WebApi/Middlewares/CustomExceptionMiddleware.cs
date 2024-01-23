using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApi.Middlewares;

class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Stopwatch watch = Stopwatch.StartNew();
        string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path;
        Console.WriteLine(message);

        try
        {
            await _next(context);

            watch.Stop();
            message = "[Response] HTTP" + context.Request.Method + " - " + context.Request.Path
                + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            Console.WriteLine(message);
        }
        catch (Exception exp)
        {
            watch.Stop();

            await HandleException(context, exp, watch);
        }
    }

    Task HandleException(HttpContext context, Exception exp, Stopwatch watch)
    {
        string message = "[Error] HTTP" + context.Request.Method + " - " + context.Response.StatusCode
            + " Error Message: " + exp.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
        Console.WriteLine(message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        string result = JsonConvert.SerializeObject(new { error = exp.Message }, Formatting.None);

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}