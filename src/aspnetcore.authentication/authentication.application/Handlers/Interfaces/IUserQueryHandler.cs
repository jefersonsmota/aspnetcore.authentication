using authentication.application.Common;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Interfaces
{
    public interface IUserQueryHandler
    {
        Task<CommandResponse> Handler(string login);
    }
}
