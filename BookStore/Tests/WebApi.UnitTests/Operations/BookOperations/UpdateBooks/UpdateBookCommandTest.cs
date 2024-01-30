using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.BookOperations.UpdateBook;
using Xunit;

namespace Operations.BookOperations.UpdateBooks;

public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;

    public UpdateBookCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidIdandBookModelIsGiven_Book_ShouldBeUpdated()
    {
        // Arrange
        int id = 1, pageCount = 250, authorId = 2, genreId = 2;
        string title = "HisLand";
        DateTime publishDate = new DateTime(2010, 5, 23);

        UpdateBookCommand command = new(_context, _mapper, id, new UpdateBookCommand.UpdateBookModel()
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
        _context.Books.SingleOrDefault(x => x.Id == id && x.Title == title && x.PageCount == pageCount && x.AuthorId == authorId && x.GenreId == genreId)
            .Should().NotBeNull();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeThrow(int? genreId)
    {
        // Arrange
        int id = 1, pageCount = 250, authorId = 2;
        string title = "HisLand";
        DateTime publishDate = new DateTime(2010, 5, 23);

        UpdateBookCommand command = new(_context, _mapper, id, new UpdateBookCommand.UpdateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = publishDate,
            AuthorId = authorId,
            GenreId = genreId
        });

        // Act & Assert
        FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre does not exist!");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeThrow(int? authorId)
    {
        // Arrange
        int id = 1, pageCount = 250, genreId = 2;
        string title = "HisLand";
        DateTime publishDate = new DateTime(2010, 5, 23);

        UpdateBookCommand command = new(_context, _mapper, id, new UpdateBookCommand.UpdateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = publishDate,
            AuthorId = authorId,
            GenreId = genreId
        });

        // Act & Assert
        FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author does not exist!");
    }
}