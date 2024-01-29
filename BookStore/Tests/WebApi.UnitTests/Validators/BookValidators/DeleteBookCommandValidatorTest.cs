using FluentAssertions;
using FluentValidation.Results;
using TestSetup;
using WebApi.Utilities.Operations.BookOperations.DeleteBook;
using WebApi.Utilities.Validators.BookValidators;
using Xunit;

namespace Validators.BookValidators;

public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenBookIdNullOrLessThenOneIsGiven_Validator_ShouldBeReturnOneError(int id)
    {
        // Arrange
        DeleteBookCommand command = new(null, id);
        DeleteBookCommandValidator validator = new();

        // Act
        ValidationResult result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
    }
}