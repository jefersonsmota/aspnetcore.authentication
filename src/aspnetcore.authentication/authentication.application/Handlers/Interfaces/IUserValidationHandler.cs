using authentication.application.Commands.User;

namespace authentication.application.Handlers.Interfaces
{
    public interface IUserValidationHandler : IBaseValidator
    {
        bool Handler(CreateUserRequest createUserRequest);
        bool Handler(SingInUserRequest singInUserRequest);
    }
}
