using FluentAssertions;
using TestSetup;
using WebApi.Utilities.Operations.GenreOperations.CreateGenres;
using WebApi.Utilities.Validators.GenreValidators;
using Xunit;

namespace Validators.GenreValidators;

public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidNameIsGiven_Validator_ShouldBeReturnErrors()
    {
        // Arrange
        CreateGenreCommand command = new(null, null, new CreateGenreCommand.CreateGenreViewModel() { Name = string.Empty });

        CreateGenreCommandValidator validator = new();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}