using FluentValidation;
using WebApi.Utilities.Operations.BookOperations.GetBooks;

namespace WebApi.Utilities.Validators.BookValidators;

public class GetBookCommandValidator : AbstractValidator<GetBookCommand>
{
    public GetBookCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}