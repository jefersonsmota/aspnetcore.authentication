using authentication.domain.Services;
using authentication.domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace authentication.domain.Entities
{
    /// <summary>
    /// Usuário domínio. 
    /// </summary>
    public class User : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Hometown { get; set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public IEnumerable<Phone> Phones { get; private set; }

        public User(string firstName, string lastName, string email, string password, string hometown, IEnumerable<Phone> phones = null, DateTime? createdAt = null, DateTime? lastLogin = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Hometown = hometown;
            CreatedAt = createdAt.HasValue ? createdAt.Value : DateTime.UtcNow;
            LastLogin = lastLogin;

            Salt = Guid.NewGuid().ToString(); 
            Password = HashPassService.GenerateSaltedHash(password, Salt);

            Phones = phones;

            Validation = new UserValidation().Validate(this);
        }

        protected User() { }

        public void AddPhone(int number, int areaCode, string countryCode)
        {
            var phone = new Phone(number, areaCode, countryCode);

            AddPhone(phone);
        }
        public void AddPhone(Phone phone)
        {
            var phones = new List<Phone>();

            if(Phones.Any())
                phones.AddRange(Phones);
            
            phones.Add(phone);

            Phones = phones;
        }

        public void AddPhone(IEnumerable<Phone> phones)
        {
            var newPhones = new List<Phone>();

            if (Phones.Any())
                newPhones.AddRange(Phones);

            newPhones.AddRange(phones);

            Phones = newPhones;
        }

        public void UpdateLastLogin()
        {
            this.LastLogin = DateTime.UtcNow;
        }

        public bool isValidPass(string checkPass)
        {
            var testPass = HashPassService.GenerateSaltedHash(checkPass, this.Salt);
            return HashPassService.CompareByteArrays(this.Password, testPass);
        }

        public override bool IsValid()
        {
            return Validation.IsValid;
        }
    }
}
