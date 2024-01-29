using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.BookOperations.DeleteBook;
using Xunit;

namespace Operations.BookOperations.DeleteBooks;

public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;

    public DeleteBookCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
    {
        // Arrange
        int id = 1;

        DeleteBookCommand command = new(_context, id);

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        _context.Books.SingleOrDefault(x => x.Id == id).Should().BeNull();

    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidBookIdIsGıven_InvalidOperationException_ShouldBeThrow(int id)
    {
        // Arrange
        DeleteBookCommand command = new(_context, id);

        // Act
        FluentActions.Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book does not exist!");
    }
}