﻿namespace authentication.domain.Validations
{
    public class UserValidation : BaseUserValidation
    {
        public UserValidation()
        {
            ValidateEmail();
            ValidateFirstName();
            ValidateLastName();
            ValidatePassword();
            ValidatePhone();
        }

        protected void ValidatePhone()
        {
            RuleForEach(p => p.Phones)
                .SetValidator(new PhoneValidation());
        }
    }
}
