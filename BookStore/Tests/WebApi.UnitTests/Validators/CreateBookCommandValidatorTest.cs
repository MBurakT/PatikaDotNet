using Xunit;
using System;
using TestSetup;
using WebApi.Utilities.Operations.BookOperations.CreateBook;
using WebApi.Utilities.Validators.BookValidators;
using FluentValidation.Results;
using FluentAssertions;

namespace Validators;

public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(" ", 0, 0, 0)]
    [InlineData("The", 100, 4, 2)]
    [InlineData("The Lord Of The Rings", 0, 4, 2)]
    [InlineData("The Lord Of The Rings", 100, 0, 2)]
    [InlineData("The Lord Of The Rings", 100, 4, 0)]
    [InlineData("The Lord Of The Rings", 100, 4, 2)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int authorId, int genreId)
    {
        // Arrange
        CreateBookCommand command = new(null, null, new CreateBookCommand.CreateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Today.AddDays(-1),
            AuthorId = authorId,
            GenreId = genreId
        });

        // Act
        CreateBookCommandValidator validator = new();

        ValidationResult result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPublishDateEqualsNowIsGiven_Validator_ShouldBeReturnOneError()
    {
        // Arrange
        CreateBookCommand command = new(null, null, new CreateBookCommand.CreateBookModel()
        {
            Title = "The Lord Of The Rings",
            PageCount = 100,
            PublishDate = DateTime.Today.AddDays(1),
            AuthorId = 4,
            GenreId = 2
        });

        // Act
        CreateBookCommandValidator validator = new();

        ValidationResult result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
    }
}