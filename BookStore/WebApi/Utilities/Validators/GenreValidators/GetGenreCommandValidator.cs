using FluentValidation;
using WebApi.Utilities.Operations.GenreOperations.GetGenres;

namespace WebApi.Utilities.Validators.GenreValidators;

public class GetGenreCommandValidator : AbstractValidator<GetGenreCommand>
{
    public GetGenreCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}