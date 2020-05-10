using authentication.application.Commands.User;
using authentication.application.Handlers.Interfaces;
using authentication.domain.Constants;
using Microsoft.EntityFrameworkCore.Internal;

namespace authentication.application.Handlers.Services
{
    public class UserValidationHandler : BaseValidationHandler, IUserValidationHandler
    {
        public UserValidationHandler()
        {
            this.ErrorMessage = string.Empty;
            this.IsValid = true;
        }
        public bool Handler(CreateUserRequest createUserRequest)
        {
            if (string.IsNullOrWhiteSpace(createUserRequest.Email)
                || string.IsNullOrWhiteSpace(createUserRequest.FirstName) 
                || string.IsNullOrWhiteSpace(createUserRequest.LastName)
                || string.IsNullOrWhiteSpace(createUserRequest.Password))
            {
                this.IsValid = false;
                this.ErrorMessage = Messages.MISSING_FIELDS;
                return this.IsValid;
            }
                
            if (createUserRequest.Phones == null || !createUserRequest.Phones.Any())
            {
                this.IsValid = false;
                this.ErrorMessage = Messages.MISSING_FIELDS;
                return this.IsValid;
            }

            if (!createUserRequest.Email.Contains("@"))
            {
                this.IsValid = false;
                this.ErrorMessage = Messages.INVALID_FIELDS;
                return this.IsValid;
            }


            return this.IsValid;
        }

        public bool Handler(SingInUserRequest singInUserRequest)
        {
            if (string.IsNullOrWhiteSpace(singInUserRequest.Email)
                || string.IsNullOrWhiteSpace(singInUserRequest.Password))
            {
                this.IsValid = false;
                this.ErrorMessage = Messages.MISSING_FIELDS;
                return this.IsValid;
            }

            if (!singInUserRequest.Email.Contains("@"))
            {
                this.IsValid = false;
                this.ErrorMessage = Messages.INVALID_FIELDS;
                return this.IsValid;
            }

            return this.IsValid;
        }

        string IBaseValidator.ErrorMessage()
        {
            return this.ErrorMessage;
        }

        bool IBaseValidator.IsValid()
        {
            return this.IsValid;
        }
    }
}
