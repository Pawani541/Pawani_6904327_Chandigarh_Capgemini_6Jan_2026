using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(3).WithMessage("Full name must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password must contain at least 1 uppercase, 1 lowercase, 1 number and 1 special character.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Please enter a valid 10-digit Indian mobile number.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Pincode)
                .NotEmpty().WithMessage("Pincode is required.")
                .Matches(@"^\d{6}$")
                .WithMessage("Pincode must be exactly 6 digits.");
        }
    }
}
