using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.BookOperations.CreateBook;
using Xunit;

namespace Operations.BookOperations.CreateBooks;


public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;

    public CreateBookCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShoudBeReturn()
    {
        // Arrange (Hazırlık)
        string title = "Test";

        _context.Books.Add(new(title, 100, new DateTime(1990, 1, 10), 1, 3));
        _context.SaveChanges();

        CreateBookCommand command = new(_context, _mapper, new CreateBookCommand.CreateBookModel() { Title = title });

        // Act (Çalıştırma) & Assert (Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book already exist!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShoulBeCreated()
    {
        // Arrange
        string title = "The Lord Of The Rings";
        int pageCount = 1000, authorId = 4, genreId = 2;
        DateTime publishDate = DateTime.Today.AddDays(-1);

        CreateBookCommand command = new(_context, _mapper, new CreateBookCommand.CreateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = publishDate,
            AuthorId = authorId,
            GenreId = genreId
        });

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        _context.Books.SingleOrDefault(
            x => x.Title.Equals(title) && x.PageCount == pageCount && x.PublishDate == publishDate && x.AuthorId == authorId && x.GenreId == genreId)
            .Should().NotBeNull();
    }
}