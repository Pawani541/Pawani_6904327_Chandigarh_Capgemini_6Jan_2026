using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validators
{
    public class BookCreateValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Book title is required.")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .Matches(@"^(?:\d{9}[\dX]|\d{13})$")
                .WithMessage("ISBN must be 10 or 13 digits.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Please select a valid category.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Please select a valid author.");

            RuleFor(x => x.PublisherId)
                .GreaterThan(0).WithMessage("Please select a valid publisher.");
        }
    }

    public class BookUpdateValidator : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Invalid book ID.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Book title is required.")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
        }
    }
}
