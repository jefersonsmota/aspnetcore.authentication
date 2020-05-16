using authentication.application.Commands.User;
using FluentValidation;

namespace authentication.application.Validations
{
    public abstract class UserCommandValidation : AbstractValidator<CreateUserRequest>
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

        protected void ValidateEmail()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is requires")
                .EmailAddress();
        }

        protected void ValidatePassword()
        {
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is requires");
        }
    }
}
