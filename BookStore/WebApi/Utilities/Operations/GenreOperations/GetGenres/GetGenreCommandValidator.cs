using FluentValidation;

namespace WebApi.Utilities.Operations.GenreOperations.GetGenres;

public class GetGenreCommandValidator : AbstractValidator<GetGenreCommand>
{
    public GetGenreCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}