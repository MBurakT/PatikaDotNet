using FluentValidation;

namespace WebApi.Operations.GenreOperations.CreateGenres;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(x => x.Genre.Name).NotNull().NotEmpty();
    }
}