using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Profiles;

namespace TestSetup;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        DbContextOptions<BookStoreDbContext> options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase("BookStoreTestDb").Options;
        Context = new BookStoreDbContext(options);

        Context.Database.EnsureCreated();
        Context.AddBooks();
        Context.AddGenres();
        Context.AddAuthors();

        Mapper = new MapperConfiguration(config =>
        {
            config.AddProfile<BookMappingProfile>();
            config.AddProfile<GenreMappingProfile>();
            config.AddProfile<AuthorMappingProfile>();
        }).CreateMapper();

        // Mapper = new MapperConfiguration(config => config.AddProfiles([new BookMappingProfile(), new GenreMappingProfile(), new AuthorMappingProfile()])).CreateMapper();
    }
}