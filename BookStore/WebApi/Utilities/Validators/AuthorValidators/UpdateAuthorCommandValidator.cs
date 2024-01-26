using FluentValidation;
using WebApi.Utilities.Operations.AuthorOperations.UpdateAuthors;

namespace WebApi.Utilities.Validators.AuthorValidators;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Author.Name).NotEmpty().Length(1, 15);
        RuleFor(x => x.Author.Surname).NotEmpty().Length(1, 15);
    }
}