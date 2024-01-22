// dotnet watch run --project Middleware/Middleware.csproj
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middleware.Middlewares;

namespace Middleware;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        // app.Run(async context => Console.WriteLine("Middleware 1")); //ShortCircuit


        // app.Use(async (context, next) =>
        // {
        //     Console.WriteLine("Middleware 1 started");
        //     await next.Invoke();
        //     Console.WriteLine("Middleware 1 completed");
        // });

        // app.Use(async (context, next) =>
        // {
        //     Console.WriteLine("Middleware 2 started");
        //     await next.Invoke();
        //     Console.WriteLine("Middleware 2 completed");
        // });

        // app.Use(async (context, next) =>
        // {
        //     Console.WriteLine("Middleware 3 started");
        //     await next.Invoke();
        //     Console.WriteLine("Middleware 3 completed");
        // });


        app.UseHello();


        app.Use(async (context, next) =>
        {
            Console.WriteLine("Use Middleware triggered.");
            await next.Invoke();
        });

        app.Map("/example", internalApp =>
            internalApp.Run(async context => //ShortCircuit
            {
                Console.WriteLine("/example Middleware triggered.");
                await context.Response.WriteAsync("/example Middleware triggered.");
            }));


        app.MapWhen(x => x.Request.Method == "GET", internalApp =>
            internalApp.Run(async context => //ShortCircuit
            {
                Console.WriteLine("MapWhen Middleware triggered.");
                await context.Response.WriteAsync("MapWhen Middleware triggered.");
            }));

        app.MapControllers();

        app.Run();
    }
}