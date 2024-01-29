using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace TestSetup;

public static class Books
{
    public static void AddBooks(this BookStoreDbContext context)
    {
        if (!context.Books.Any())
        {
            context.Books.AddRange(new List<Book>()
                {
                    new Book("Lean Startup", 200, new DateTime(2001, 6, 12), 1, 3),
                    new Book("Herland", 250, new DateTime(2010, 5, 23), 2, 2),
                    new Book("Dune", 540, new DateTime(2002, 12, 21), 3, 2)
                });

            context.SaveChanges();
        }
    }
}