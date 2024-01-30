using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.BookOperations.GetBooks;
using Xunit;

namespace Operations.BookOperations.GetBooks;

public class GetBookCommandTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;

    public GetBookCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidBookIdIsGiven_BookViewModel_ShouldBeReturn()
    {
        // Arrange
        int id = 1;

        GetBookCommand command = new(_context, _mapper, id);

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().NotBeNull();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeThrow(int id)
    {
        GetBookCommand command = new(_context, _mapper, id);

        FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book does not exist!");
    }
}