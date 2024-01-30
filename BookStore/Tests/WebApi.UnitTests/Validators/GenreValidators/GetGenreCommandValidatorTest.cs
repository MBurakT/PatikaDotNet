using FluentAssertions;
using TestSetup;
using WebApi.Utilities.Operations.GenreOperations.GetGenres;
using WebApi.Utilities.Validators.GenreValidators;
using Xunit;

namespace Validators.GenreValidators;

public class GetGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
    {
        // Arrange
        int id = 2;

        GetGenreCommand command = new(null, null, id);
        GetGenreCommandValidator validator = new();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}