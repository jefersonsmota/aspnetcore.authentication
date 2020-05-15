using authentication.domain.Entities;
using FluentValidation;
using System;

namespace authentication.domain.Validations
{
    public class PhoneValidation : AbstractValidator<Phone>
    {
        protected void ValidateNumber()
        {
            RuleFor(p => p.Number)
                .NotNull()
                .NotEqual(0);
        }

        protected void ValidadeAreaCode()
        {
            RuleFor(p => p.AreaCode)
                .NotNull().NotEqual(0);
        }

        protected void ValidateCountryCode()
        {
            RuleFor(p => p.CountryCode)
                .NotEmpty();
        }

        protected void ValidateUserId()
        {
            RuleFor(p => p.UserId)
                .NotNull()
                .NotEqual(Guid.Empty);
        }
    }
}
