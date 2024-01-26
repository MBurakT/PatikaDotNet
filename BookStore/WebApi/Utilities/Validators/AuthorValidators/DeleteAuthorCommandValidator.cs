using FluentValidation;
using WebApi.Utilities.Operations.AuthorOperations.DeleteAuthors;

namespace WebApi.Utilities.Validators.AuthorValidators;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}