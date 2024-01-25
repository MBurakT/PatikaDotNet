using FluentValidation;

namespace WebApi.Operations.GenreOperations.UpdateGenres;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Genre.Name).NotNull().NotEmpty();
    }
}