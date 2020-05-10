using authentication.application.Commands.User;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Interfaces
{
    public interface IUserQueryHandler
    {
        Task<UserResponse> Handler(string login);
    }
}
