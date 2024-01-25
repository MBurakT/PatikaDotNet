using FluentValidation;

namespace WebApi.Operations.GenreOperations.GetGenres;

public class GetGenreCommandValidator : AbstractValidator<GetGenreCommand>
{
    public GetGenreCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}