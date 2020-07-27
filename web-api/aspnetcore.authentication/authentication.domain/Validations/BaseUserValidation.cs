using authentication.domain.Entities;
using FluentValidation;

namespace authentication.domain.Validations
{
    public class BaseUserValidation : AbstractValidator<User>
    {
        protected void ValidateFirstName()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First Name is requires")
                .Length(2, 150).WithMessage("The First Name must have between 2 and 150 characters");
        }

        protected void ValidateLastName()
        {
            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last Name is requires")
                .Length(2, 150).WithMessage("The Last Name must have between 2 and 150 characters");
        }

        protected void ValidateHometown()
        {
            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Hometown is requires")
                .Length(2, 150).WithMessage("The Hometown must have between 2 and 150 characters");
        }

        protected void ValidateEmail()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is requires")
                .EmailAddress().WithMessage("Email is not valid");
        }

        protected void ValidatePassword()
        {
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is requires");
        }
    }
}
