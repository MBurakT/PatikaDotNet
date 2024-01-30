using FluentAssertions;
using TestSetup;
using WebApi.Utilities.Operations.BookOperations.GetBooks;
using WebApi.Utilities.Validators.BookValidators;
using Xunit;

namespace Validators.BookValidators;

public class GetBookCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenValidIdIsGiven_Validator_ShouldBeNoError()
    {
        // Arrange
        int id = 1;

        GetBookCommand command = new(null, null, id);
        GetBookCommandValidator validator = new();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(null)]
    public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError(int id)
    {
        // Arrange
        GetBookCommand command = new(null, null, id);
        GetBookCommandValidator validator = new();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
    }
}