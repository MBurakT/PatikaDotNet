using FluentValidation;

namespace WebApi.Operations.BookOperations.GetBooks;

class GetBookCommandValidator : AbstractValidator<GetBookCommand>
{
    public GetBookCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}