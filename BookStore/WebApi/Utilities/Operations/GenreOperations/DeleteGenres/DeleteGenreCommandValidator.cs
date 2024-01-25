using FluentValidation;

namespace WebApi.Utilities.Operations.GenreOperations.DeleteGenres;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}