using FluentValidation;
using WebApi.Utilities.Operations.GenreOperations.CreateGenres;

namespace WebApi.Utilities.Validators.GenreValidators;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(x => x.Genre.Name).NotNull().NotEmpty();
    }
}