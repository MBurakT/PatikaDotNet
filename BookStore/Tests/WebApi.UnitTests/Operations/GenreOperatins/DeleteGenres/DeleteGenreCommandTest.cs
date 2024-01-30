using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.GenreOperations.DeleteGenres;
using Xunit;

namespace Operations.GenreOperations.DeleteGenres;

public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;

    public DeleteGenreCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenValidIdIsGiven_Book_ShouldBeRemoved()
    {
        // Arrange
        int id = 2;

        DeleteGenreCommand command = new(_context, id);

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert

        _context.Genres.Any(x => x.Id == id).Should().BeFalse();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown(int id)
    {
        // Arrange
        DeleteGenreCommand command = new(_context, id);

        // Act && Assert
        FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre does not exist!");
    }
}