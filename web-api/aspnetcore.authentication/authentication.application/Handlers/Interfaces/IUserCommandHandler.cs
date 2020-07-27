using authentication.application.Commands.Request.User;
using authentication.application.Commands.User;
using authentication.application.Common;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Interfaces
{
    public interface IUserCommandHandler
    {
        Task<CommandResponse> Handler(CreateUserRequest command);
        Task<CommandResponse> Handler(SingInUserRequest singIn);
        Task<CommandResponse> Handler(MeRequest login);
        Task<CommandResponse> Handler(ForgotPasswordRequest forgot);
    }
}
