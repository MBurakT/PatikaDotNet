using FluentValidation;
using WebApi.Utilities.Operations.GenreOperations.DeleteGenres;

namespace WebApi.Utilities.Validators.GenreValidators;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}