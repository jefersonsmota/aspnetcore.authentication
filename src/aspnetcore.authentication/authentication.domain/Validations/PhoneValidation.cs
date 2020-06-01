namespace authentication.domain.Validations
{
    public class PhoneValidation : BasePhoneValidation
    {
        public PhoneValidation()
        {
            ValidateNumber();
            ValidateCountryCode();
            ValidateAreaCode();
        }
    }
}
