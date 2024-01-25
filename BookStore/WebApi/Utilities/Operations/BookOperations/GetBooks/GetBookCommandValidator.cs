using FluentValidation;

namespace WebApi.Utilities.Operations.BookOperations.GetBooks;

class GetBookCommandValidator : AbstractValidator<GetBookCommand>
{
    public GetBookCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}