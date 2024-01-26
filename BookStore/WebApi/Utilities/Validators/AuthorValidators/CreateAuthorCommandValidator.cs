using FluentValidation;
using WebApi.Utilities.Operations.AuthorOperations.CreateAuthors;

namespace WebApi.Utilities.Validators.AuthorValidators;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.Author.Name).NotEmpty().Length(1, 15);
        RuleFor(x => x.Author.Surname).NotEmpty().Length(1, 15);
    }
}