using FluentValidation;
using WebApi.Utilities.Operations.BookOperations.DeleteBook;

namespace WebApi.Utilities.Validators.BookValidators;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}