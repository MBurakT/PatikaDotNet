using FluentValidation;

namespace WebApi.Operations.BookOperations.DeleteBook;

class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0);
    }
}