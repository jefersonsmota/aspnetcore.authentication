using authentication.domain.Entities;
using FluentValidation;
using System;

namespace authentication.domain.Validations
{
    public class BasePhoneValidation : AbstractValidator<Phone>
    {
        protected void ValidateNumber()
        {
            RuleFor(p => p.Number)
                .NotNull()
                .GreaterThanOrEqualTo(0).WithMessage("Number is required");
        }

        protected void ValidateAreaCode()
        {
            RuleFor(p => p.AreaCode)
                .NotNull().GreaterThanOrEqualTo(0).WithMessage("Area code is required");
        }

        protected void ValidateCountryCode()
        {
            RuleFor(p => p.CountryCode)
                .NotNull()
                .NotEmpty().WithMessage("Country code is required");
        }

        protected void ValidateUserId()
        {
            RuleFor(p => p.UserId)
                .NotNull()
                .NotEqual(Guid.Empty);
        }
    }
}
