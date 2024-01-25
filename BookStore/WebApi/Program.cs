// dotnet watch run --project BookStore/WebApi/WebApi.csproj
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Utilities.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddDbContext<BookStoreDbContext>(opt => opt.UseInMemoryDatabase("BookStoreDB"));
        //builder.Services.AddTransient<DataSeeder>();

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddSingleton<ILoggerService, DbLogger>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (IServiceScope serviceScope = app.Services.CreateScope())
        {
            DataSeeder.Seed(serviceScope.ServiceProvider);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCustomExceptionMiddleware();

        app.MapControllers();

        // Console.WriteLine(Assembly.GetExecutingAssembly());
        // Console.WriteLine(typeof(Program).Assembly);
        // Console.WriteLine();

        app.Run();
    }
}