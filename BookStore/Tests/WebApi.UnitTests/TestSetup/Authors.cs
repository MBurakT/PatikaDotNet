using System.Linq;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
        if (!context.Authors.Any())
        {
            context.Authors.AddRange(
                [
                    new Author("Eric", "Ries"),
                    new Author("Charlotte", "Gilman"),
                    new Author("Frank", "Herbert"),
                    new Author("John Ronald Reuel", "Tolkien")
                ]);

            context.SaveChanges();
        }
    }
}