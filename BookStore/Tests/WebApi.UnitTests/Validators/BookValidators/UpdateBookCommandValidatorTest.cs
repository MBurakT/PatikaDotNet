using System;
using System.Globalization;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.BookOperations.UpdateBook;
using WebApi.Utilities.Validators.BookValidators;
using Xunit;

namespace Validators.BookValidators;

public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    readonly BookStoreDbContext _context;
    readonly IMapper _mapper;

    public UpdateBookCommandValidatorTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData("His", 120, "2010-05-23", 2, 2)]
    [InlineData("HisLand", 0, "2010-05-23", 2, 2)]
    [InlineData("HisLand", 120, "2500-05-23", 2, 2)]
    [InlineData("HisLand", 120, "2010-05-23", 0, 2)]
    [InlineData("HisLand", 120, "2010-05-23", 2, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string? title, int? pageCount, string? publishDateText, int? authorId, int? genreId)
    {
        // Arrange
        int id = 1;

        DateTime.TryParseExact(publishDateText, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime publishDate);

        UpdateBookCommand command = new(_context, _mapper, id, new UpdateBookCommand.UpdateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = publishDate == default ? null : publishDate,
            AuthorId = authorId,
            GenreId = genreId
        });

        UpdateBookCommandValidator validator = new();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }


    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldBeReturnNoError()
    {
        // Arrange
        int id = 1, pageCount = 120, authorId = 2, genreId = 2;
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

        UpdateBookCommandValidator validator = new();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
    }
}