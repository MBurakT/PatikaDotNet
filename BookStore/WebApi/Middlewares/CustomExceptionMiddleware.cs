using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Utilities.Services;

namespace WebApi.Middlewares;

class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _loggerService;

    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        _next = next;
        _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
        Stopwatch watch = Stopwatch.StartNew();
        string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path;
        _loggerService.Write(message);

        try
        {
            await _next(context);

            watch.Stop();
            message = "[Response] HTTP" + context.Request.Method + " - " + context.Request.Path
                + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);
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
        _loggerService.Write(message);

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