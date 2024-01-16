using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Webapi.Models;

namespace WebApi.DBOperations;

public class DataSeeder
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using (BookStoreDbContext context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if (context.Books.Any()) return;

            context.Books.AddRange(new List<Book>()
            {
                new Book(1, "Lean Startup", 200, new DateTime(2001, 6, 12), 1),
                new Book(2, "Herland", 250, new DateTime(2010, 5, 23), 2),
                new Book(3, "Dune", 540, new DateTime(2002, 12, 21), 2)
            });

            context.SaveChanges();
        }
    }
}


