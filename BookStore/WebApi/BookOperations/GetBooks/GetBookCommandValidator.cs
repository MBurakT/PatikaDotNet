using FluentValidation;

namespace WebApi.BookOperations.GetBooks;

class GetBookCommandValidator : AbstractValidator<GetBookCommand>
{
    public GetBookCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0);
    }
}