using FluentAssertions;
using TestSetup;
using WebApi.Utilities.Operations.GenreOperations.UpdateGenres;
using WebApi.Utilities.Validators.GenreValidators;
using Xunit;

namespace Validators.GenreValidators;

public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors(int id)
    {
        // Arrange
        UpdateGenreCommand command = new(null, null, id, new UpdateGenreCommand.UpdateGenreViewModel() { Name = "Drama" });

        UpdateGenreCommandValidator validator = new();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}