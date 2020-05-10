using authentication.application.Commands.User;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Interfaces
{
    public interface IUserCommandHandler
    {
        Task<int> Handler(CreateUserRequest command);
        Task<UserResponse> Handler(SingInUserRequest singIn);
        Task<UserResponse> Handler(string login);
    }
}
