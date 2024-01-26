using FluentValidation;
using WebApi.Utilities.Operations.BookOperations.GetBooks;

namespace WebApi.Utilities.Validators.BookValidators;

class GetBookCommandValidator : AbstractValidator<GetBookCommand>
{
    public GetBookCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}