using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validators
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Invalid user.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.BookId)
                    .GreaterThan(0).WithMessage("Invalid book selected.");

                item.RuleFor(x => x.Qty)
                    .GreaterThan(0).WithMessage("Quantity must be at least 1.")
                    .LessThanOrEqualTo(100).WithMessage("Cannot order more than 100 of a single book.");
            });
        }
    }
}
