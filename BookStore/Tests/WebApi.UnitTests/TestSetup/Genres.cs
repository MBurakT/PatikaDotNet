using System.Linq;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace TestSetup;

public static class Genres
{
    public static void AddGenres(this BookStoreDbContext context)
    {
        if (!context.Genres.Any())
        {
            context.Genres.AddRange(
                [
                    new Genre("None"),
                    new Genre("Science Fiction"),
                    new Genre("Personal Growth"),
                    new Genre("Novel")
                ]);

            context.SaveChanges();
        }
    }
}