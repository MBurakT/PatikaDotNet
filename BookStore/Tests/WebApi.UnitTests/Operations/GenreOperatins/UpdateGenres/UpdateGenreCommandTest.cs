using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.GenreOperations.UpdateGenres;
using Xunit;

namespace Operations.GenreOperations.UpdateGenres;

public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;

    public UpdateGenreCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown(int id)
    {
        UpdateGenreCommand command = new(_context, _mapper, id, new UpdateGenreCommand.UpdateGenreViewModel() { Name = "Drama" });

        FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre does not exist!");
    }
}