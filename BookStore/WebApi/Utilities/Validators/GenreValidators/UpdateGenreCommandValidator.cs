using FluentValidation;
using WebApi.Utilities.Operations.GenreOperations.UpdateGenres;

namespace WebApi.Utilities.Validators.GenreValidators;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Genre.Name).NotNull().NotEmpty();
    }
}