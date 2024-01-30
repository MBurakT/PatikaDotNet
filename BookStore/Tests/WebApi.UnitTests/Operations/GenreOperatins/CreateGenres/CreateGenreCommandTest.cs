using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.GenreOperations.CreateGenres;
using Xunit;

namespace Operations.GenreOperations.CreateGenres;

public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;

    public CreateGenreCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistNameIsGiven_InvalidOperationException_ShouldBeThrown()
    {
        string name = "Science Fiction";

        // Arrange
        CreateGenreCommand command = new(_context, _mapper, new CreateGenreCommand.CreateGenreViewModel() { Name = name });

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre already exists!");
    }

    [Fact]
    public void WhenValidNameIsGiven_Book_ShouldBeCreated()
    {
        string name = "Fantasy";

        // Arrange
        CreateGenreCommand command = new(_context, _mapper, new CreateGenreCommand.CreateGenreViewModel() { Name = name });

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        _context.Genres.Any(x => x.Name == name).Should().BeTrue();
    }
}