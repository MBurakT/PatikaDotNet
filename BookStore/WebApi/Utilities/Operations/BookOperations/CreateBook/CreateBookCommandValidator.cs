using FluentValidation;

namespace WebApi.Utilities.Operations.BookOperations.CreateBook;

class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.BookModel.Title).NotNull().NotEmpty().MinimumLength(4);
        RuleFor(x => x.BookModel.PageCount).NotNull().GreaterThan(0);
        RuleFor(x => x.BookModel.PublishDate).NotNull().LessThan(System.DateTime.Today);
        RuleFor(x => x.BookModel.GenreId).NotNull().GreaterThan(0);
    }
}