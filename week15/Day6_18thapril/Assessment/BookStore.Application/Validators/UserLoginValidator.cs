using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}
