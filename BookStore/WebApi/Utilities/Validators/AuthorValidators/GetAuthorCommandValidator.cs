using FluentValidation;
using WebApi.Utilities.DBOperations;
using WebApi.Utilities.Operations.AuthorOperations.GetAuthors;

namespace WebApi.Utilities.Validators.AuthorValidators;

public class GetAuthorCommandValidator : AbstractValidator<GetAuthorCommand>
{
    public GetAuthorCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}