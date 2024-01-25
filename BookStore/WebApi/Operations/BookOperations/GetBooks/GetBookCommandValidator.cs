using FluentValidation;

namespace WebApi.Operations.BookOperations.GetBooks;

class GetBookCommandValidator : AbstractValidator<GetBookCommand>
{
    public GetBookCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0);
    }
}