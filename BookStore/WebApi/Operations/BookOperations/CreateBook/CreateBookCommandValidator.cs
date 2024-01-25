using System;
using FluentValidation;
using static WebApi.Operations.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.BookOpertions.CreateBook;

class CreateBookCommandValidator : AbstractValidator<CreateBookModel>
{
    public CreateBookCommandValidator()
    {
        RuleFor(bookModel => bookModel.GenreId).NotNull().GreaterThan(0);
        RuleFor(bookModel => bookModel.PageCount).NotNull().GreaterThan(0);
        RuleFor(bookModel => bookModel.PublishDate).NotEmpty().LessThan(DateTime.Today);
        RuleFor(bookModel => bookModel.Title).NotEmpty().MinimumLength(4);
    }
}