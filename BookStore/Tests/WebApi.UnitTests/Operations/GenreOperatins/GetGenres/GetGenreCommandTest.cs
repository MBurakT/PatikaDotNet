using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.GenreOperations.GetGenres;
using Xunit;

namespace Operations.GenreOperations.GetGenres;

public class GetGenreCommandTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;

    public GetGenreCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown(int id)
    {
        // Arrange
        GetGenreCommand command = new(_context, _mapper, id);

        // Act & Assert
        FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre does not exist!");
    }
}