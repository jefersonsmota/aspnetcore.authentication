using authentication.domain.Validations;
using System;

namespace authentication.domain.Entities
{
    /// <summary>
    /// Telefones domínio.
    /// </summary>
    public class Phone : Entity
    {
        public int Number { get; private set; }
        public int AreaCode { get; private set; }
        public string CountryCode { get; private set; }

        public Guid UserId { get; private set; }

        public virtual User User { get; protected set; }

        public Phone(int number, int areaCode, string countryCode)
        {
            Number = number;
            AreaCode = areaCode;
            CountryCode = countryCode;

            Validation = new PhoneValidation().Validate(this);
        }

        protected Phone() { }

        public override bool IsValid()
        {
            return Validation.IsValid;
        }
    }
}
