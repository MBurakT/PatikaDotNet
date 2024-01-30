using FluentAssertions;
using TestSetup;
using WebApi.Utilities.Operations.GenreOperations.DeleteGenres;
using WebApi.Utilities.Validators.GenreValidators;
using Xunit;

namespace Validators.GenreValidators;

public class DeleteGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError(int id)
    {
        // Arrange 
        DeleteGenreCommand command = new(null, id);
        DeleteGenreCommandValidator validator = new();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}